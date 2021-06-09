open Newtonsoft.Json
open System.IO
open Newtonsoft.Json.Linq
open System.Text.RegularExpressions
open Newtonsoft.Json.Schema
open System
open System.Collections.Generic
open FSharp.Compiler.SourceCodeServices
open FSharp.Compiler.Text
open FSharp.Compiler.SyntaxTree
open FSharp.Compiler.SyntaxTree
open Microsoft.FSharp.Quotations

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
      return JSchema.Parse("{}") 
    | Path file ->
      use sr = new StreamReader(file)
      use jr = new JsonTextReader(sr)
      let settings = JSchemaReaderSettings(ResolveSchemaReferences = false, Resolver = JSchemaUrlResolver(), BaseUri = Uri("https://json.schemastore.org/kustomization.json"))
      let t = JSchema.Load(jr)
      return t
  }


type Node =
 | Property of Property
 | Other of JSchema
and Property = string * JSchema * bool

[<EntryPoint>]
let main argv =
  let schemaUrl = UrlOrFilePath.ofString argv.[0]
  let schema = loadJson schemaUrl |> Async.RunSynchronously

  printfn "#r \"nuget: Yzl\""
  printfn "open Yzl.Core"
  printfn ""
  printf "type Kustomization = "
  
  let capitalize (x:string) = Char.ToUpper(x.[0]).ToString() + x.[1..]

  let (|Typed|_|) (s:JSchema) =
    match s with
    | x when x.Type.HasValue -> Some (x.Type.Value, x)
    | _ -> None


  let (|Properties|_|) (s:JSchema) =
    match s.Properties |> Seq.toList with
    | [] -> None
    | xs -> Some (xs, s)

  let (|PatternProperties|_|) (s:JSchema) =
    match s.PatternProperties |> Seq.toList with
    | [] -> None
    | xs -> Some (xs, s)

  let (|Ref|_|) (s:JSchema) =
    match s with
    | x when x.Ref |> isNull |> not -> Some (x.Ref, x)
    | _ -> None

  let (|OfType|_|) (schemaType:JSchemaType) (s:JSchema) =
    match s with
    | Typed (typ, o) when typ = schemaType -> Some o 
    | _ -> None

  let (|OneOf|_|) (s:JSchema) =
    match s.OneOf |> Seq.toList with
    | [] -> None
    | xs -> Some (xs, s)

  let (|Enum|_|) (s:JSchema) =
    match s.Enum |> Seq.toList with
    | [] -> None
    | xs -> Some (xs, s)

  let types = HashSet<string>()

  let rec renderValue (x:Node) =
    
    let enum x =
      let Key = x |> string |> capitalize 
      printf "\n | %s" Key

    let duCase (x:KeyValuePair<string,JSchema>) =
      let Key = capitalize x.Key
      printf " | %s of %s" Key Key

      match x.Value with
      | OfType JSchemaType.Array _ ->
        printf " seq"
      | _ -> ()
      printfn ""

    let renderProperty (x:KeyValuePair<string,JSchema>) =
      
      let Key = capitalize x.Key
      if types.Add Key then
        if x.Value.Description |> isNull |> not then printfn "\n/// %s" x.Value.Description
        printf "and %s = " Key
        renderValue (Property(x.Key, x.Value, false))
        printfn ""

    let renderz (parentKey:string option) (value:JSchema) =
      match value with
      | Ref (ref, s) ->
        printfn "ref"
      | OneOf (items, s) ->
        items |> Seq.iter (Other >> renderValue)
      | Enum (items, s) ->
        items |> Seq.iter enum
      | OfType JSchemaType.String x -> printf "string"
      | OfType JSchemaType.Boolean x -> printf "bool"
      | OfType JSchemaType.Number x -> printf "float"
      | OfType JSchemaType.Integer x -> printf "int"
      | OfType JSchemaType.Array x ->
        x.Items |> Seq.iter (Other >> renderValue)
        //printf " seq"
      | PatternProperties (items, k) ->
        printf "Yzl.NamedNode list"
      | Properties (items, x) ->
        let key = parentKey |> Option.defaultValue ""
        if key = "" || types.Add key then
          printfn ""
          items |> Seq.iter duCase
          printfn ""
          items |> Seq.iter renderProperty
      | OfType JSchemaType.Object x ->
        printf "obj"
      
      | _ -> printf "unit"

    match x with
    | Property (name, value, isRoot) -> renderz (Some name) value
    | Other value -> renderz None value


  renderValue (Property ("kustomization", schema, true))

 
//   let formatAst = Fantomas.CodeFormatter.FormatASTAsync
    
//   let checker = FSharpChecker.Create()
//   let file = "/home/queil/code/yzl/src/Yzl.Bindings.Gen/out2.fsx"
//   let opts = {
//     FSharpParsingOptions.Default with
//       SourceFiles = [|file|]
//   }
//   let text = """namespace Yzl.Bindings

// open Yzl.Core

// module Kustomize =
//   type Kustomizaton =
//    | Args of Args
//   and Args = Yzl.Str

//   """

//   let x = checker.ParseFile (file, SourceText.ofString text, opts) |> Async.RunSynchronously
  
//   printfn "%A" x.ParseTree.Value
  
  

//   let tree = ParsedInput.ImplFile (ParsedImplFileInput ("", false, QualifiedNameOfFile (Ident()), [], [], [], (true,true)))
  
//   let output = formatAst ( x.ParseTree.Value, "out2.fsx", [], None, Fantomas.FormatConfig.FormatConfig.Default) |> Async.RunSynchronously
//   //schema |> renderValue
  

  0
