open System
open System.Collections.Generic
open NJsonSchema
open System.Text

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



// type Ctx2() =
//   member _.Types : List<YzlType> = List<YzlType>()
//   member _.

type Context =
  {
    EntryModuleName: string
    ParentType: YzlType option
    AllTypes: YzlType list
    AllFuncs: YzlFunc list
  }

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

  // let rec render (s:JsonSchema) (ctx: Context) =
  //   match s with
  //   | Definitions (def) ->
  //     def |> Seq.iter (fun (k,v) ->
  //       let desc =
  //         match v.Description with
  //         | d when d |> String.IsNullOrWhiteSpace -> ""
  //         | d -> sprintf " - %s" d
  //       printfn "\n/// %s%s" k desc
  //       // if ctx.EntryModuleName = k 
  //       //   then printfn "[<AutoOpen>]"
  //       //   else ()
  //       printf "type %s() = " k
  //       render v { ctx with ParentKey = k }
  //       printfn ""
  //       printfn "  static member Default = %s()" k
  //       printfn "  static member yzl (build:(%s -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f %s.Default) |> Yzl.lift" k k
  //     )
  //   | Reference (ref) ->
  //     //let def = resolveDef ref
      
  //     printf "Yzl.map" //(camelize def.Key)
  //   | Array x ->
  //     //render x ctx
  //     printf "Yzl.seq"
  //   | OneOf xs ->
  //     //printf "/// oneof"
  //     let key = "union"
  //     let key = sprintf "%s%i" key (nextIndex ()) |> capitalize
      
  //     enums.Add((key, xs |> List.map (fun x -> 
  //       let def = resolveDef x.Reference
  //       def.Key
  //     )))
  //     printf "%s" key
  //   | Array2 xs ->
  //     printf "/// weird"
  //   | Enum (xs,v) ->
  //     let key =
  //       match v with
  //       | :? JsonSchemaProperty as p -> p.Name 
  //       | _ -> "AnonymousEnum"
      
  //     let key = sprintf "%s%i" key  (nextIndex ()) |> capitalize
  //     enums.Add((key, xs |> List.map string))
  //     printf "Yzl.str" //let's ignore type information for now
  //   | String x ->
  //     printf "Yzl.str"
  //   | Boolean x ->
  //     printf "Yzl.boolean"
  //   | PatternProperties xs ->
  //     xs |> Seq.iter (fun _ -> printf "Yzl.map")
  //   | Properties xs ->
  //     xs |> Seq.iter (fun (k,v) -> 
  //       let key = camelize k
        
  //       let rec annotation =
  //         function
  //         | String _ -> "^a"
  //         | Enum _ -> "string"
  //         | Boolean _ -> "bool"
  //         | Array a -> 
  //           let nested = annotation a
  //           sprintf "%s list" nested
  //         | PatternProperties _ -> "Yzl.NamedNode list"
          
  //         //| Reference ref -> 
  //           //let def = resolveDef ref 
  //           //sprintf "%s -> Yzl.NamedNode list" (def.Key)
  //           //sprintf ""
  //         | Reference ref ->
  //           let def = resolveDef ref
  //           sprintf "(%s -> Yzl.NamedNode) list" (def.Key)
  //         | _ -> "Yzl.Node" 

  //       let value =
  //         function
  //         | Reference ref -> 
  //            let def = resolveDef ref 
  //            sprintf"(value |> List.map (fun f -> f %s.Default))" (def.Key)
  //         | Array a ->
  //           match a with
  //           | Reference ref ->
  //             let def = resolveDef ref 
  //             sprintf"(value |> List.map (fun v -> v |> List.map (fun f -> f %s.Default)) |> Yzl.liftMany)" (def.Key)
  //           | _ -> "(value |> Yzl.liftMany)"

  //         |_ -> "value"

  //       printfn ""
  //       // match ctx.Type with
  //       // | RecordDefinition ->
  //       let escapedKey =
  //         match key with
  //         | "namespace" | "type" -> sprintf "``%s``" key
  //         | _ -> key
        
  //       match v.Description with
  //       | d when d |> String.IsNullOrWhiteSpace -> ()
  //       | d -> printfn "  /// %s" d
  //       printf """  static member inline %s (value: %s) (_:%s) = """ escapedKey (annotation v) ctx.ParentKey
  //       render v ctx
  //       printf """ "%s" %s""" key (value v)

  //       // | DefaultRecord -> printf "      %s = None" key
  //       // | Default -> ()
  //     )

  //   | x -> failwithf "No idea what to do! %A" x
  
  
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

        let ctx = metadata s {
          ctx 
            with 
              ParentType = Some yzlType
          }

        {
          ctx with
            AllTypes = match ctx.ParentType with | None -> ctx.AllTypes | Some t -> t::ctx.AllTypes
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


  let ctx = metadata schema {ParentType = None; AllFuncs =[]; AllTypes = []; EntryModuleName = ""}
  

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
               | NamedNodeFuncList _ -> sprintf "value |> Yzl.convertSeqBinding"
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

          yield! t.Functions |> Seq.collect (fun f -> 
            [
              newLine
              yield! match f.Description with | Some d -> ["  /// "; d; newLine] |_ -> []
              "  static member inline "
              f.Name |> escapeFSharpKeywords; " "
              "(value: "; renderTypeAnnotation f; ") "
              "(_: "; t.Name ;")"
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
  ctx |> render
  
  0
