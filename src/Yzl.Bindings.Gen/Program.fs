open System
open System.Collections.Generic
open NJsonSchema

type YzlMetadata = {
  AllTypes: YzlType list
  AllFunctions: YzlFunc list
} and YzlType = {
  Name: string
  Description: string option
  Functions: YzlFunc list
} and YzlFunc = {
  Name: string
  Description: string option
  Kind: YzlKind
  Parent: YzlType option
} and YzlKind = 
  | String
  | Int
  | Float
  | Node
  | NamedNodeFuncList of name: string
  | NamedNodeList
  | Seq of YzlKind
  | Boolean
  | Enum

type Context =
  {
    EntryModuleName: string
    ParentType: YzlType option
    AllTypes: YzlType list
    AllFuncs: YzlFunc list
  }

[<Literal>]
let CommonTypeName = "Common"

type UrlOrFilePath =
 | Path of string
 | Url of string
with 
  static member ofString (value:string) =
    match value with
    | x when x.StartsWith("https://") || x.StartsWith("http://") -> Url x
    | _ -> Path value

let loadJson (url:UrlOrFilePath) =
  async {
    let! token = Async.CancellationToken
    
    match url with 
    | Url url -> 
      failwithf "not supported yet"
      return NJsonSchema.JsonSchema.CreateAnySchema()
    | Path file ->
      let! x =  (NJsonSchema.JsonSchema.FromFileAsync(file, token) |> Async.AwaitTask)
      return x
  }

[<EntryPoint>]
let main argv =
  let schemaUrl = UrlOrFilePath.ofString argv.[0]
  let schema = loadJson schemaUrl |> Async.RunSynchronously
  let resolveDef ref = schema.Definitions |> Seq.find (fun (KeyValue(_,v)) -> v = ref)

  let capitalize (x:string) = Char.ToUpper(x.[0]).ToString() + x.[1..]
  let camelize (x:string) = Char.ToLower(x.[0]).ToString() + x.[1..]

  let matchNonEmpty =
   function
    | null -> None
    | defs ->
      match defs |> Seq.toList |> List.map (|KeyValue|) with
      | [] -> None
      | xs -> Some xs

  let matchType (typ:JsonObjectType) (x:JsonSchema) =
    match x with
    | x when x.Type = typ -> Some x
    | _ -> None 

  let (|Properties|_|) (s:JsonSchema) =
    s.Properties |> matchNonEmpty

  let (|PatternProperties|_|) (s:JsonSchema) =
    s.PatternProperties |> matchNonEmpty

  let (|Array2|_|) (s:JsonSchema) =
    match s.Items |> Seq.toList with
    | [] -> None
    | xs -> Some xs

  let (|OneOf|_|) (s:JsonSchema) =
    match s.OneOf |> Seq.toList with
    | [] -> None
    | xs -> Some xs

  let (|Array|_|) (s:JsonSchema) =
    match s.Item with
    | null -> None
    | v -> Some v

  let (|Enum|_|) (s:JsonSchema) =
    match s.Enumeration |> Seq.toList with
    | [] -> None
    | v -> Some (v, s)

  let (|String|_|) (s:JsonSchema) =
   s |> matchType JsonObjectType.String

  let (|Boolean|_|) (s:JsonSchema) =
   s |> matchType JsonObjectType.Boolean

  let (|Reference|_|) (s:JsonSchema) =
    match s with
    | x when x.Reference |> isNull |> not -> Some (x.Reference, x.Reference |> resolveDef)
    | _ -> None

  let (|Definitions|_|) (s:JsonSchema) =
    s.Definitions |> matchNonEmpty


  
  let enums = List<(string * string list)>()
  
  let nextIndex =
    let mutable z = 0
    fun () -> 
      z <- z+1
      z

  let rec metadata (s:JsonSchema) (ctx: Context) =
    let toOption = function | d when String.IsNullOrWhiteSpace d -> None | d -> Some d

    match s with
    | Definitions defs ->
      defs |> Seq.fold (fun ctx (k,s) ->
        
        let yzlType = {
          Name = k
          Description = s.Description |> toOption
          Functions = []
        }

        let ctx = metadata s { ctx with ParentType = Some yzlType }

        {
          ctx with AllTypes = match ctx.ParentType with | None -> ctx.AllTypes | Some t -> t::ctx.AllTypes
        }
      ) ctx

    | Properties xs ->
     
       xs |> Seq.fold (fun ctx (k, s) -> 
        
        let rec dataType (s':JsonSchema) =
          match s' with
          | String _ -> String
          | Enum _ -> Enum
          | Boolean _ -> Boolean
          | Array x -> Seq (dataType x)
          | PatternProperties _ -> NamedNodeList
          | Reference (_, def) -> NamedNodeFuncList def.Key
          | _ -> Node
         
        let yzlFunc =
          {
            Name = k
            Description = s.Description |> toOption
            Kind = dataType s
            Parent = ctx.ParentType
          }
        {
          ctx with 
            AllFuncs = yzlFunc::ctx.AllFuncs
            ParentType = 
              match ctx.ParentType with
              | None -> None
              | Some t -> Some { 
                t with Functions = yzlFunc::t.Functions
              }
        }
       ) ctx
    |_ -> ctx

  let ctx = metadata schema {ParentType = None; AllFuncs = []; AllTypes = []; EntryModuleName = ""}

  let commonType = {
    Name = CommonTypeName
    Description = Some "Common tags"
    Functions = []
  } 

  let commonFuncs = 
    ctx.AllFuncs 
    |> List.groupBy (fun x -> {Name=x.Name; Kind=x.Kind; Parent = Some commonType; Description = None} )
    |> List.filter (fun (_, v) -> v.Length > 1)

  let commonType =
    {
      commonType with
        Functions = commonFuncs |> List.map (fun (k,_) -> k)
    }

  let render (x:Context) =

    let newLine = "\n"

    let escapeFSharpKeywords =
      function
      | "namespace" | "type" as s -> sprintf "``%s``" s
      | s -> s

    let renderTypeAnnotation (f:YzlFunc) =
      let rec kindToType =
        function
        | YzlKind.String -> "^a"
        | YzlKind.Enum -> "string"
        | YzlKind.Boolean -> "bool"
        | YzlKind.Seq kind -> sprintf "%s list" <| kindToType kind
        | YzlKind.NamedNodeList -> "Yzl.NamedNode list"
        | YzlKind.NamedNodeFuncList key -> sprintf "(%s -> Yzl.NamedNode) list" key
        | _ -> "Yzl.Node"
      kindToType f.Kind

    let yzlFunc (f: YzlFunc) =
      match f.Kind with
      | YzlKind.String _ -> "Yzl.str"
      | YzlKind.Enum _ -> "Yzl.str"
      | YzlKind.Seq _ -> "Yzl.seq"
      | YzlKind.Boolean _ -> "Yzl.boolean"
      | YzlKind.NamedNodeFuncList _
      | YzlKind.NamedNodeList -> "Yzl.map"
      | _ -> "Yzl.booom"

    let renderImpl (f: YzlFunc) =
      let rec kindToImpl =
        function
        | NamedNodeFuncList key -> sprintf "(value |> List.map (fun f -> f %s.Default))" key
        | Seq kind -> 
          sprintf "(%s |> Yzl.liftMany)"
            <| match kind with
               | NamedNodeFuncList name -> sprintf "value |> List.map (fun fs -> fs |> List.map(fun f -> f %s.Default))" name
               | _ -> kindToImpl kind
        |_ -> "value"
      kindToImpl f.Kind
    
    let renderAdditionalMembers (t: YzlType) = [
      "  static member Default = "; t.Name ;"()"
      newLine
      "  static member yzl (build:("; t.Name; " -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f "; t.Name; ".Default) |> Yzl.lift"
      newLine
    ]
    
    let allStrings =
      x.AllTypes |> Seq.collect (fun t -> 

        [
          yield! match t.Description with | Some d -> ["/// "; d; newLine] |_ -> []
          "type "; t.Name; "() ="; 

          yield! t.Functions
            |> match t.Name with 
               | CommonTypeName -> Seq.map id
               | _ -> Seq.filter (fun f -> commonFuncs |> Seq.exists (fun (k, _) -> k.Name = f.Name && k.Kind = f.Kind ) |> not)
            |> Seq.collect (fun f -> 
            [
              newLine
              yield! match f.Description with | Some d -> ["  /// "; d; newLine] |_ -> []
              "  static member inline "
              f.Name |> escapeFSharpKeywords; " "
              "(value: "; renderTypeAnnotation f; ") "
              "(_: "; (match t.Name with | CommonTypeName -> "'b" | _ -> t.Name); ")"
              " = "
              yzlFunc f; " \""; f.Name; "\""
              " "
              renderImpl f
            ]
          )
          newLine
          yield! renderAdditionalMembers t
        ]
      )
    (allStrings |> String.concat "") |> printfn "%s"


  printfn "namespace rec Yzl.Bindings.Kustomize"
  printfn "open Yzl.Core"
  {ctx with AllTypes = commonType::ctx.AllTypes} |> render
  
  0
