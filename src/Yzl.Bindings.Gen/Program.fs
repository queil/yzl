open System
open System.IO
open System.Net.Http
open NJsonSchema
open Yzl.Bindings.Gen
open Argu

type YzlType =
    { Name: string
      Description: string option
      Functions: YzlFunc list }

and YzlFunc =
    { Name: string
      Description: string option
      Kind: SchemaKind
      Parent: YzlType option }

and SchemaKind =
    | String
    | Int
    | Float
    | Node
    | Reference of name: string
    | PatternProperties
    | Seq of SchemaKind
    | Boolean
    | Enum
    | InlineObject

type Context =
    { EntryModuleName: string
      ParentType: YzlType option
      AllTypes: YzlType list
      AllFuncs: YzlFunc list }

type UrlOrFilePath =
    | Path of string
    | Url of string

    static member ofString(value: string) =
        match value with
        | x when x.StartsWith("https://") || x.StartsWith("http://") -> Url x
        | _ -> Path value

/// Derive a valid F# type identifier from a potentially dotted name.
/// e.g. "io.k8s.api.apps.v1.Deployment" -> "Deployment"
/// Strips leading non-identifier characters and replaces others with underscores.
let toTypeName (name: string) =
    let last = name.Split([| '.' |]) |> Array.last
    let stripped = last.TrimStart([| for c in last do if not (Char.IsLetter c || c = '_') then yield c |])
    let sanitized = stripped |> String.map (fun c -> if Char.IsLetterOrDigit c || c = '_' then c else '_')
    if sanitized = "" then "_" else sanitized

let loadJson (url: UrlOrFilePath) =
    async {
        let! token = Async.CancellationToken

        match url with
        | Url url ->
            use client = new HttpClient()
            let! json = client.GetStringAsync(url) |> Async.AwaitTask
            let! x = JsonSchema.FromJsonAsync(json) |> Async.AwaitTask
            return x
        | Path file ->
            let! x = JsonSchema.FromFileAsync(file, token) |> Async.AwaitTask
            return x
    }

type Args =
    | [<MainCommand; ExactlyOnce>] Schema of schema: string
    | [<AltCommandLine("-n")>] Namespace of ``namespace``: string
    | [<AltCommandLine("-o")>] Output of file: string
    | [<AltCommandLine("-f")>] Filter of prefix: string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Schema _ -> "URL or file path to JSON schema"
            | Namespace _ -> "F# namespace for generated code (default: Yzl.Bindings)"
            | Output _ -> "Output file path (default: stdout)"
            | Filter _ -> "Only generate types whose definition name starts with <prefix>, e.g. io.k8s.api.apps.v1"

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<Args>(programName = "yzl-gen")

    let results =
        try
            parser.ParseCommandLine(argv) |> Some
        with :? ArguParseException as ex ->
            match ex.ErrorCode with
            | ErrorCode.HelpText -> exit 0
            | _ ->
                eprintfn "%s" ex.Message
                None

    match results with
    | None -> 1
    | Some results ->

    let schemaPath = results.GetResult Schema
    let namespaceName = results.GetResult(Namespace, defaultValue = "Yzl.Bindings")
    let outputFile = results.TryGetResult Output
    let filterPrefix = results.TryGetResult Filter

    let schema = loadJson (UrlOrFilePath.ofString schemaPath) |> Async.RunSynchronously

    let rec metadata (s: JsonSchema) (ctx: Context) =
        let toOption =
            function
            | d when String.IsNullOrWhiteSpace d -> None
            | d -> Some d

        match s with
        | Patterns.Definitions defs ->
            defs
            |> Seq.filter (fun (k, _) ->
                match filterPrefix with
                | Some prefix -> k.StartsWith(prefix)
                | None -> true)
            |> Seq.fold
                (fun ctx (k, s) ->

                    let yzlType =
                        { Name = toTypeName k
                          Description = s.Description |> toOption
                          Functions = [] }

                    let ctx = metadata s { ctx with ParentType = Some yzlType }

                    { ctx with
                        AllTypes =
                            match ctx.ParentType with
                            | None -> ctx.AllTypes
                            | Some t -> t :: ctx.AllTypes })
                ctx

        | Patterns.Properties xs ->

            let rootTypeCreated = ctx.ParentType.IsNone

            let ctx =
                if rootTypeCreated then
                    let typeName =
                        match s.Description with
                        | null | "" -> "Root"
                        | d ->
                            d.Split([| ' '; '\n'; '\r'; '.'; '"' |], StringSplitOptions.RemoveEmptyEntries)
                            |> Array.tryFind (fun w ->
                                w.Length > 1
                                && Char.IsUpper w.[0]
                                && w |> Seq.forall Char.IsLetterOrDigit)
                            |> Option.defaultValue "Root"

                    { ctx with ParentType = Some { Name = typeName; Description = s.Description |> toOption; Functions = [] } }
                else
                    ctx

            let ctx =
                xs
                |> Seq.fold
                    (fun ctx (k, s) ->

                        let rec dataType (s': JsonSchema) =
                            match s' with
                            | Patterns.Integer _ -> Int
                            | Patterns.Number _ -> Float
                            | Patterns.String _ -> String
                            | Patterns.Enum _ -> Enum
                            | Patterns.Boolean _ -> Boolean
                            | Patterns.Array x -> Seq(dataType x)
                            | Patterns.PatternProperties _ -> PatternProperties
                            | Patterns.Reference ref ->
                                let def =
                                    schema.Definitions
                                    |> Seq.tryFind (fun (KeyValue(_, v)) -> v = ref)

                                match def with
                                | Some d -> Reference(toTypeName d.Key)
                                | None -> Node
                            | Patterns.Object _ -> InlineObject
                            | _ -> Node

                        let yzlFunc =
                            { Name = k
                              Description = s.Description |> toOption
                              Kind = dataType s
                              Parent = ctx.ParentType }

                        // Recursively create sub-types for inline nested objects
                        let ctx =
                            match s with
                            | Patterns.Properties _ ->
                                let subType =
                                    { Name = toTypeName k
                                      Description = s.Description |> toOption
                                      Functions = [] }

                                let savedParent = ctx.ParentType
                                let ctxAfterSub = metadata s { ctx with ParentType = Some subType }

                                match ctxAfterSub.ParentType with
                                | None -> ctxAfterSub
                                | Some completedSub ->
                                    let alreadyExists =
                                        ctxAfterSub.AllTypes |> List.exists (fun t -> t.Name = completedSub.Name)

                                    { ctxAfterSub with
                                        AllTypes =
                                            if alreadyExists then ctxAfterSub.AllTypes
                                            else completedSub :: ctxAfterSub.AllTypes
                                        ParentType = savedParent }
                            | _ -> ctx

                        { ctx with
                            AllFuncs = yzlFunc :: ctx.AllFuncs
                            ParentType =
                                match ctx.ParentType with
                                | None -> None
                                | Some t ->
                                    Some
                                        { t with
                                            Functions = yzlFunc :: t.Functions } })
                    ctx

            if rootTypeCreated then
                match ctx.ParentType with
                | None -> ctx
                | Some t -> { ctx with AllTypes = t :: ctx.AllTypes; ParentType = None }
            else
                ctx

        | _ -> ctx

    let ctx =
        metadata
            schema
            { ParentType = None
              AllFuncs = []
              AllTypes = []
              EntryModuleName = "" }

    let render (x: Context) =

        let newLine = "\n"

        let escapeFSharpKeywords =
            function
            | "namespace"
            | "type"
            | "default"
            | "when"
            | "inherit"
            | "interface"
            | "abstract"
            | "override"
            | "member"
            | "module"
            | "open"
            | "begin"
            | "end"
            | "in"
            | "let"
            | "do"
            | "new"
            | "base"
            | "val"
            | "rec"
            | "and"
            | "match"
            | "with"
            | "for"
            | "while"
            | "try"
            | "finally"
            | "use"
            | "if"
            | "then"
            | "else"
            | "fun"
            | "function"
            | "return"
            | "yield"
            | "static"
            | "class"
            | "struct"
            | "true"
            | "false"
            | "not"
            | "or"
            | "parallel" as s -> sprintf "``%s``" s
            | s -> s

        let renderTypeAnnotation (f: YzlFunc) =
            let rec kindToType =
                function
                | SchemaKind.Int -> "int"
                | SchemaKind.Float -> "float"
                | SchemaKind.String -> "string"
                | SchemaKind.Enum -> "string"
                | SchemaKind.Boolean -> "bool"
                | SchemaKind.Seq kind -> sprintf "%s list" <| kindToType kind
                | SchemaKind.PatternProperties -> "NamedNode list"
                | SchemaKind.Reference _ -> "NamedNode list"
                | SchemaKind.InlineObject -> "NamedNode list"
                | _ -> "Node"

            kindToType f.Kind

        let yzlFunc (f: YzlFunc) =
            match f.Kind with
            | SchemaKind.Int -> "Yzl.int"
            | SchemaKind.Float -> "Yzl.float"
            | SchemaKind.String -> "Yzl.str"
            | SchemaKind.Enum -> "Yzl.str"
            | SchemaKind.Seq _ -> "Yzl.seq"
            | SchemaKind.Boolean -> "Yzl.boolean"
            | SchemaKind.Reference _
            | SchemaKind.PatternProperties
            | SchemaKind.InlineObject -> "Yzl.map"
            | _ -> "Yzl.named"

        let renderImpl (f: YzlFunc) =
            let rec kindToImpl =
                function
                | Reference _ -> "value"
                | _ -> "value"

            kindToImpl f.Kind

        let renderAdditionalMembers (t: YzlType) =
            [ "  static member Default = "
              t.Name |> escapeFSharpKeywords
              "()"
              newLine
              "  static member yzl (build:NamedNode list) : Node = build |> lift"
              newLine ]

        let normalizeDescription (d: string) =
            d.Split([| '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
            |> String.concat " "

        let renderFunc (f: YzlFunc) =
            let render (typeAnnotation: YzlFunc -> string) =
                [ newLine
                  yield!
                      match f.Description with
                      | Some d -> [ "  /// "; normalizeDescription d; newLine ]
                      | _ -> []
                  "  static member "
                  f.Name |> escapeFSharpKeywords
                  " "
                  "(value: "
                  typeAnnotation f
                  ") "
                  " = "
                  yzlFunc f
                  "("
                  renderImpl f
                  ", \""
                  f.Name
                  "\")" ]

            [ yield! render renderTypeAnnotation
              match f.Kind with
              | String -> yield! render (fun _ -> "Str")
              | _ -> () ]

        let allStrings =
            x.AllTypes
            |> Seq.collect (fun t ->

                [ yield!
                      match t.Description with
                      | Some d -> [ "/// "; normalizeDescription d; newLine ]
                      | _ -> []
                  "type "
                  t.Name |> escapeFSharpKeywords
                  "() ="

                  yield!
                      t.Functions
                      |> Seq.collect renderFunc
                  newLine
                  yield! renderAdditionalMembers t ])

        let renderBuildersType () =
            let allFuncs =
                x.AllTypes
                |> List.collect (fun t -> t.Functions)
                |> List.distinctBy (fun f -> (f.Name, renderTypeAnnotation f))

            let renderMember (f: YzlFunc) =
                let render (typeAnnotation: YzlFunc -> string) =
                    [ newLine
                      "  static member "
                      f.Name |> escapeFSharpKeywords
                      " (value: "
                      typeAnnotation f
                      ")  = "
                      yzlFunc f
                      "(value, \""
                      f.Name
                      "\")" ]

                [ yield! render renderTypeAnnotation
                  match f.Kind with
                  | String -> yield! render (fun _ -> "Str")
                  | _ -> () ]

            [ newLine
              "type Builders() ="
              yield! allFuncs |> List.collect renderMember
              newLine ]

        Seq.append allStrings (renderBuildersType ()) |> String.concat ""

    let header = $"// Auto-generated by Yzl.Bindings.Gen - do not edit manually.\n// Source schema: {schemaPath}\nnamespace rec {namespaceName}\nopen Yzl.Core\n\n"

    let body = ctx |> render

    let output = header + body

    match outputFile with
    | Some path -> File.WriteAllText(path, output)
    | None -> printf "%s" output

    0
