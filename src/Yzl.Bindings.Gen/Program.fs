open Newtonsoft.Json
open System.IO
open Newtonsoft.Json.Linq
open System.Text.RegularExpressions

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
      return JObject() :> JToken
    | Path file ->
      use sr = new StreamReader(file)
      use jr = new JsonTextReader(sr)
      let! t = JToken.ReadFromAsync(jr, token) |> Async.AwaitTask
      return t
  }

  


[<EntryPoint>]
let main argv =
  let schemaUrl = UrlOrFilePath.ofString argv.[0]
  let schema = loadJson schemaUrl |> Async.RunSynchronously


  let (|JObject|_|) (token: JToken) =
    match token.Type with
    | JTokenType.Object -> Some(token :?> JObject)
    | _ -> None

  let (|JObjectChildOf|_|) (parentPath:string) (token: JToken) =
    match token with
    | x when x.Parent |> isNull -> None
    | :? JObject as o when o.Parent.Path = parentPath -> Some o
    | _ -> None


  let (|JArray|_|) (token: JToken) =
    match token.Type with
    | JTokenType.Array -> Some(token :?> JArray)
    | _ -> None

  let (|JProperty|_|) (token: JToken) =
    match token.Type with
    | JTokenType.Property -> Some(token :?> JProperty)
    | _ -> None


  let (|JStringChildOf|_|) (parentPathRegex:string) (token: JToken) =
    match token with
    | x when x.Parent |> isNull -> None
    | :? JValue as o 
      when o.Type = JTokenType.String && Regex.IsMatch(o.Parent.Path, parentPathRegex) 
      -> Some((token :?> JValue).Value |> string)
    | _ -> None

  let (|JPropertyChildOf|_|) (parentPath:string) (token: JToken) =
    match token with
    | x when x.Parent |> isNull -> None
    | :? JProperty as o when o.Parent.Path = parentPath -> Some o
    | _ -> None
 
  let (|JPropertyAt|_|) (pathRegex:string) (token: JToken) =
    match token with
    | :? JProperty as o when Regex.IsMatch(o.Path, pathRegex) -> Some o
    | _ -> None


  let schemaVisitor (t:JToken) =
    match t with


    //| JArray a -> printfn "%s" "array"
   // | JProperty "$ref" p -> printfn "%s" p.Path
   // | JObject o -> printfn "%s" "object"
    //| JObjectChildOf "definitions" o -> printfn "OF YEAH -> %s" o.Path
    | JPropertyChildOf "definitions" p ->
      printfn "%s" p.Name
    | JStringChildOf "^definitions\.[^.]*\.type$" s -> 
      printfn "%A" s
    | x -> printfn "  %s - (%A)" x.Path x.Type


  let walk t (visit:JToken -> unit) =

    let rec walk (t:JToken) =
      match t with
      | JObject o ->
        visit o
        o.Children<JProperty>() |> Seq.iter walk
      | JArray a ->
        visit a
        a.Children() |> Seq.iter walk
      | JProperty p -> 
        visit p
        walk p.Value 
      | x -> visit x

    walk t

  walk schema schemaVisitor
    
          

  // let settings = YzlGeneratorSettings()
  // let gen = YzlGenerator(schema, YzlTypeResolver(settings), settings)
  // let tsGen = TypeScriptGenerator(schema, TypeScriptGeneratorSettings
  //               (
  //                   TypeStyle = TypeScriptTypeStyle.Interface,
  //                   TypeScriptVersion = 1.8m
  //               ))
  // //let file = tsGen.GenerateFile()
  // let file = gen.GenerateFile()
  //printf "%s" file
  0
