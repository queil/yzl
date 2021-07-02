
open System.IO
open Newtonsoft.Json.Linq
open System.Text.RegularExpressions

open System
open System.Collections.Generic
open FSharp.Compiler.SourceCodeServices
open FSharp.Compiler.Text
open FSharp.Compiler.SyntaxTree
open FSharp.Compiler.SyntaxTree
open Microsoft.FSharp.Quotations
open NJsonSchema


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
      // use sr = new StreamReader(file)
      // use jr = new JsonTextReader(sr)
      // let settings = JSchemaReaderSettings(ResolveSchemaReferences = false, Resolver = JSchemaUrlResolver(), BaseUri = Uri("https://json.schemastore.org/kustomization.json"))
      // let t = JSchema.Load(jr, settings)
      let! x =  (NJsonSchema.JsonSchema.FromFileAsync(file, token) |> Async.AwaitTask)
      return x
  }


// type Node =
//  | Property of Property
//  | Other of JSchema
// and Property = string * JSchema * bool

[<EntryPoint>]
let main argv =
  let schemaUrl = UrlOrFilePath.ofString argv.[0]
  let schema = loadJson schemaUrl |> Async.RunSynchronously


  printfn "#r \"nuget: Yzl\""
  printfn "namespace rec Yzl.Bindings.Kustomize"
  printfn "open Yzl.Core"
  printfn ""

  let types = HashSet<string>()
  let capitalize (x:string) = Char.ToUpper(x.[0]).ToString() + x.[1..]

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
    | x when x.Reference |> isNull |> not -> Some (x.Reference)
    | _ -> None

  let (|Definitions|_|) (s:JsonSchema) =
    s.Definitions |> matchNonEmpty

  let resolveDef ref = schema.Definitions |> Seq.find (fun (KeyValue(k,v)) -> v = ref)
  
  let enums = List<(string * string list)>()
  
  let nextIndex =
    let mutable z = 0
    fun () -> 
      z <- z+1
      z

  let rec render (s:JsonSchema) =
    match s with
    | Definitions (def) ->
      def |> Seq.iter (fun (k,v) ->
        printfn "\n/// Definition: %s" k
        printf "type %s = " k
        render v
        printfn ""
      )
    | Reference (ref) ->
      let def = resolveDef ref
      printf "%s" def.Key
    | Array x ->
      render x
      printf " seq"
    | OneOf xs ->
      //printf "/// oneof"
      let key = "union"
      let key = sprintf "%s%i" key  (nextIndex ()) |> capitalize
      
      enums.Add((key, xs |> List.map (fun x -> 
        let def = resolveDef x.Reference
        def.Key
      )))
      printf "%s" key
    | Array2 xs ->
      printf "/// weird"
    | Enum (xs,v) ->
      let key =
        match v with
        | :? JsonSchemaProperty as p -> p.Name 
        | _ -> "AnonymousEnum"
      
      let key = sprintf "%s%i" key  (nextIndex ()) |> capitalize
      enums.Add((key, xs |> List.map string))
      printf "%s" key
    | String x ->
      printf "(string -> Yzl.Scalar)"
    | Boolean x ->
      printf "(bool -> Yzl.Scalar)"
    | PatternProperties xs ->
      xs |> Seq.iter (fun _ -> printf "Yzl.NamedNode list")
    | Properties xs ->
      xs |> Seq.iter (fun (k,v) -> 
        let Key = capitalize k
        printf "\n | %s of " Key
        render v
      )
    | x -> failwithf "No idea what to do! %A" x
  
  render schema

  enums |> Seq.iter (fun (key, values) -> 
    printfn "type %s = %s" key (values |> Seq.map (capitalize >> (sprintf "| %s ")) |> String.concat "")
  )

  

  0
