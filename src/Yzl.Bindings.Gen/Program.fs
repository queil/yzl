open System
open NJsonSchema
open Yzl.Bindings.Gen

type YzlType = {
  Name: string
  Description: string option
  Functions: YzlFunc list
} and YzlFunc = {
  Name: string
  Description: string option
  Kind: SchemaKind
  Parent: YzlType option
} and SchemaKind = 
  | String
  | Int
  | Float
  | Node
  | Reference of name: string
  | PatternProperties
  | Seq of SchemaKind
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

  let rec metadata (s:JsonSchema) (ctx: Context) =
    let toOption = function | d when String.IsNullOrWhiteSpace d -> None | d -> Some d

    match s with
    | Patterns.Definitions defs ->
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

    | Patterns.Properties xs ->
     
       xs |> Seq.fold (fun ctx (k, s) -> 
        
        let rec dataType (s':JsonSchema) =
          match s' with
          | Patterns.Integer _ -> Int
          | Patterns.Number _ -> Float
          | Patterns.String _ -> String
          | Patterns.Enum _ -> Enum
          | Patterns.Boolean _ -> Boolean
          | Patterns.Array x -> Seq (dataType x)
          | Patterns.PatternProperties _ -> PatternProperties
          | Patterns.Reference ref -> 
            let def = schema.Definitions |> Seq.find (fun (KeyValue(_,v)) -> v = ref)
            Reference def.Key
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
        | SchemaKind.Int -> "int"
        | SchemaKind.Float -> "float"
        | SchemaKind.String -> "string"
        | SchemaKind.Enum -> "string"
        | SchemaKind.Boolean -> "bool"
        | SchemaKind.Seq kind -> sprintf "%s list" <| kindToType kind
        | SchemaKind.PatternProperties -> "Yzl.NamedNode list"
        | SchemaKind.Reference key -> "Yzl.NamedNode list"
        | _ -> "Yzl.Node"
      kindToType f.Kind

    let yzlFunc (f: YzlFunc) =
      match f.Kind with
      | SchemaKind.Int -> "Yzl.int"
      | SchemaKind.Float -> "Yzl.float"
      | SchemaKind.String _ -> "Yzl.str"
      | SchemaKind.Enum _ -> "Yzl.str"
      | SchemaKind.Seq _ -> "Yzl.seq"
      | SchemaKind.Boolean _ -> "Yzl.boolean"
      | SchemaKind.Reference _
      | SchemaKind.PatternProperties -> "Yzl.map"
      | k -> failwithf "Cannot handle kind: %A" k

    let renderImpl (f: YzlFunc) =
      let rec kindToImpl =
        function
        | Reference key -> "value"
        | Seq kind -> 
          sprintf "(%s |> Yzl.liftMany)"
            <| match kind with
               | Reference name -> "value"
               | _ -> kindToImpl kind
        |_ -> "value"
      kindToImpl f.Kind
    
    let renderAdditionalMembers (t: YzlType) = [
      "  static member Default = "; t.Name ;"()"
      newLine
      "  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift"
      newLine
    ]
    
    let renderFunc (f: YzlFunc) =
      let render (typeAnnotation:YzlFunc -> string) =
        [
          newLine
          yield! match f.Description with | Some d -> ["  /// "; d; newLine] |_ -> []
          "  static member inline "
          f.Name |> escapeFSharpKeywords; " "
          "(value: "; typeAnnotation f; ") "
          //"(_: "; (match t.Name with | CommonTypeName -> "'b" | _ -> t.Name); ")"
          " = "
          yzlFunc f; " \""; f.Name; "\""
          " "
          renderImpl f
        ]
      [
        yield! render renderTypeAnnotation
        match f.Kind with | String -> yield! render (fun _ -> "Yzl.Str") |_ -> ()
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
            |> Seq.collect renderFunc
          newLine
          yield! renderAdditionalMembers t
        ]
      )
    (allStrings |> String.concat "") |> printfn "%s"


  printfn "namespace rec Yzl.Bindings.Kustomize"
  printfn "open Yzl.Core"
  {ctx with AllTypes = commonType::ctx.AllTypes} |> render
  
  0
