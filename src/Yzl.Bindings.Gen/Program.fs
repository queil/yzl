open Newtonsoft.Json
open System.IO
open Newtonsoft.Json.Linq
open System.Text.RegularExpressions
open Newtonsoft.Json.Schema
open System
open System.Collections.Generic


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

  let rec renderValue (x:JSchema) =
    
    let renderEnum x =
      let Key = x |> string |> capitalize 
      printf "\n | %s" Key

    let renderType (x:KeyValuePair<string,JSchema>) =
      let Key = capitalize x.Key
      printf " | %s of %s" Key Key

      match x.Value with
      | OfType JSchemaType.Array _ ->
        printf " seq"
      | _ -> ()

    let renderProperty (x:KeyValuePair<string,JSchema>) = 
      let Key = capitalize x.Key
      if x.Value.Description |> isNull |> not then printfn "\n/// %s" x.Value.Description
      printfn "and %s = " Key
      renderValue x.Value
      printfn ""


    match x with
    | Ref (ref, s) ->
      printfn "ref"
    | OneOf (items, s) ->
      items |> Seq.iter renderValue
    | Enum (items, s) ->
      items |> Seq.iter renderEnum
    | OfType JSchemaType.String x -> printf "Yzl.Str"
    | OfType JSchemaType.Boolean x -> printf "Yzl.Bool"
    | OfType JSchemaType.Number x -> printf "Yzl.Float"
    | OfType JSchemaType.Integer x -> printf "Yzl.Int"
    | OfType JSchemaType.Array x ->
      x.Items |> Seq.iter renderValue
      //printf " seq"
    | Properties (items, x) ->
      items |> Seq.iter (renderType)
      printfn ""
      items |> Seq.iter (renderProperty)
    | OfType JSchemaType.Object x ->
      printf "obj"
    
    | _ -> printf "unit"



  //Fantomas.CodeFormatter.FormatASTAsync
    

  schema |> renderValue
  

  0
