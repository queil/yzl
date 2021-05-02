open System
open NJsonSchema
open Yzl.Bindings.Gen.Generator

type UrlOrFilePath =
 | Path of string
 | Url of string
with 
  static member ofString (value:string) =
    match value with
    | x when x.StartsWith("https://") || x.StartsWith("http://") -> Url x
    | _ -> Path value

let loadSchema (url:UrlOrFilePath) =
  async {
    let! token = Async.CancellationToken
    return! 
      Async.AwaitTask (
        match url with 
        | Url url -> JsonSchema.FromUrlAsync(url, token) 
        | Path file -> JsonSchema.FromFileAsync(file, token))
  }

[<EntryPoint>]
let main argv =
  let schemaUrl = UrlOrFilePath.ofString argv.[0]
  let schema = loadSchema schemaUrl |> Async.RunSynchronously

  let settings = YzlGeneratorSettings()
  let gen = YzlGenerator(schema, YzlTypeResolver(settings), settings)
  let file = gen.GenerateFile()
  printf "%s" file
  0
