namespace Yzl.Patch

open System.IO
open YamlDotNet.RepresentationModel
open YamlDotNet.Serialization
open YamlDotNet.Core
open Yzl
open System.Collections.Generic

[<RequireQualifiedAccess>]
module Patch =
  open System.Text.RegularExpressions
  open YamlDotNet.Core.Events

  let private withStyle (style: ScalarStyle) (node: YamlScalarNode) =
    node.Style <- style
    node

  let private parseSegment (seg: string) =
    let predicateMatch =
      Regex.Match(seg, @"^\[([^=]+)=(?:""([^""]*)""|'([^']*)'|([^\]]+))\]$")

    let scalarMatch = Regex.Match(seg, @"^\[(?:""([^""]*)""|'([^']*)'|([^\]]+))\]$")

    if predicateMatch.Success then
      let key = predicateMatch.Groups.[1].Value

      let value =
        if predicateMatch.Groups.[2].Success then
          predicateMatch.Groups.[2].Value
        elif predicateMatch.Groups.[3].Success then
          predicateMatch.Groups.[3].Value
        else
          predicateMatch.Groups.[4].Value

      None, Some(key, value)
    elif scalarMatch.Success then

      let value =
        if scalarMatch.Groups.[1].Success then
          scalarMatch.Groups.[1].Value
        elif scalarMatch.Groups.[2].Success then
          scalarMatch.Groups.[2].Value
        else
          scalarMatch.Groups.[3].Value

      None, Some(null, value)
    else
      Some seg, None

  let private getSegments jsonPath =
    Regex.Split(jsonPath, @"\.(?![^\[]*\])")
    |> Array.filter (fun s -> s <> "")
    |> Array.map parseSegment
    |> Array.toList

  let private resolveNestedKey (keyPath: string) (node: YamlNode) =
    match node with
    | :? YamlMappingNode as m ->
      let literalKey = YamlScalarNode keyPath

      if m.Children.ContainsKey literalKey then
        Some m.Children.[literalKey]
      else
        let rec go (parts: string list) (n: YamlNode) =
          match parts with
          | [] -> Some n
          | p :: rest ->
            match n with
            | :? YamlMappingNode as m ->
              let k = YamlScalarNode p

              if m.Children.ContainsKey k then
                go rest m.Children.[k]
              else
                None
            | _ -> None

        go (keyPath.Split('.') |> Array.toList) node
    | _ -> None

  /// Converts Yzl tree into YamlDotNet
  let fromYzl (sourceStart: Mark) (node: Node) =
    let rec traverse =
      function
      | MapNode xs ->
        let map =
          YamlMappingNode(
            seq {
              for Named(Name name, x) in xs do
                yield (YamlScalarNode name, traverse x) |> KeyValuePair<YamlNode, YamlNode>
            }
          )

        map.Style <- MappingStyle.Block
        map :> YamlNode
      | SeqNode xs ->
        let seq =
          YamlSequenceNode(
            seq {
              for x in xs do
                yield traverse x
            }
          )

        seq.Style <- SequenceStyle.Block
        seq :> YamlNode
      | Scalar s ->
        let scalar =
          match s with
          | Bool x -> x |> string |> _.ToLowerInvariant() |> YamlScalarNode
          | Int x -> x |> string |> YamlScalarNode
          | Float x -> x |> string |> YamlScalarNode
          | Str s ->
            let format (p: string) =
              let lines = p.Split([| '\n' |])
              let nonEmpty = lines |> Array.filter (fun x -> x.Trim() <> "")
              let baseIndent =
                if nonEmpty.Length = 0 then 0
                else nonEmpty |> Array.map (fun x -> x.Length - x.TrimStart().Length) |> Array.min
              lines
              |> Array.map (fun x -> if x.Trim() = "" then "" else x.[baseIndent..])
              |> Array.skipWhile (fun x -> x = "")
              |> fun arr -> arr |> Array.rev |> Array.skipWhile (fun x -> x = "") |> Array.rev
              |> String.concat "\n"

            match s with
            | Plain p ->
              let formatted = p |> format
              let node = formatted |> YamlScalarNode
              if formatted.Contains "\n" then node |> withStyle ScalarStyle.Literal else node
            | FoldedStrip p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Folded
            | FoldedKeep p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Folded
            | Folded p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Folded
            | Literal p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Literal
            | LiteralKeep p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Literal
            | LiteralStrip p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.Literal
            | DoubleQuoted p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.DoubleQuoted
            | SingleQuoted p -> p |> format |> YamlScalarNode |> withStyle ScalarStyle.SingleQuoted

        scalar :> YamlNode
      | _ -> failwithf "Do not know"

    traverse node


  let (|MapNode|SeqNode|ScalarNode|) (node: YamlNode) =
    match node with
    | :? YamlMappingNode as x -> MapNode x
    | :? YamlSequenceNode as x -> SeqNode x
    | :? YamlScalarNode as x -> ScalarNode x
    | _ -> failwithf "Node %s is not supported" (node.NodeType |> string)

  let mergeFromYzl (source: Node) (target: YamlNode) =

    let kv (node: NamedNode) =
      let (Named(Name key, value)) = node
      YamlScalarNode key, value

    let map =
      function
      | Yzl.Core.MapNode _ ->
        let map = YamlMappingNode()
        map.Style <- MappingStyle.Block
        map :> YamlNode
      | Yzl.Core.SeqNode _ ->
        let seq = YamlSequenceNode()
        seq.Style <- SequenceStyle.Block
        seq :> YamlNode
      | Yzl.Core.Scalar _ -> YamlScalarNode() :> YamlNode
      | NoNode as z -> failwithf "Not supported %A" z

    let rec traverse (source: Node) (target: YamlNode) (parentMapWithKey: (YamlScalarNode * YamlMappingNode) option) =
      match source, target with
      | Yzl.Core.MapNode xs, MapNode m ->
        xs
        |> Seq.map kv
        |> Seq.iter (fun (k, v) ->
          let newOrExisting =
            if not <| m.Children.ContainsKey k then
              v |> map
            else
              m.Children.[k]

          m.Children.[k] <- traverse v newOrExisting (Some(k, m)))

        m :> YamlNode
      | Yzl.Core.MapNode xs, _ ->
        let m = YamlMappingNode()

        xs
        |> Seq.map kv
        |> Seq.iter (fun (k, v) ->
          let newOrExisting =
            if not <| m.Children.ContainsKey k then
              v |> map
            else
              m.Children.[k]

          m.Children.[k] <- traverse v newOrExisting (Some(k, m)))

        m :> YamlNode
      | Yzl.Core.SeqNode xs, SeqNode s ->
        xs
        |> Seq.iter (fun n ->
          let xn = n |> fromYzl target.Start

          if not <| s.Children.Contains xn then
            s.Children.Add xn
          else
            parentMapWithKey |> Option.iter (fun (k, m) -> m.Children.[k] <- xn))

        s.Style <- SequenceStyle.Block
        s :> YamlNode
      | Yzl.Core.Scalar _ as z, ScalarNode _ -> z |> fromYzl (Mark())
      | Yzl.Core.Scalar _ as z, null -> z |> fromYzl (Mark())
      | a, b -> failwithf "Merge failed. Node type mismatch: %A vs %A" a b

    traverse source target None

  let mergeFromYzlAtPath (source: Node) (target: YamlNode) (path: string) =
    let segments =
      if System.String.IsNullOrEmpty path then
        []
      else
        getSegments path

    let rec navigateTo (node: YamlNode) (segs: (string option * (string * string) option) list) =
      match segs with
      | [] -> mergeFromYzl source node |> ignore
      | seg :: rest ->
        match node, seg with

        | MapNode m, (Some propName, None) ->
          let key = YamlScalarNode propName

          if m.Children.ContainsKey key then
            if List.isEmpty rest then
              m.Children.[key] <- mergeFromYzl source m.Children.[key]
            else
              navigateTo m.Children.[key] rest
          else
            failwithf "Path not found: property '%s' does not exist" propName

        | SeqNode s, (Some idx, None) ->
          match System.Int32.TryParse idx with
          | true, index when index >= 0 && index < s.Children.Count ->
            if List.isEmpty rest then
              s.Children.[index] <- mergeFromYzl source s.Children.[index]
            else
              navigateTo s.Children.[index] rest
          | _ -> failwithf "Invalid array index: '%s' (length %d)" idx s.Children.Count

        | SeqNode s, (None, Some(key, value)) when key <> null ->
          let matchingIdx =
            s.Children
            |> Seq.tryFindIndex (fun child ->
              resolveNestedKey key child
              |> Option.map (fun n ->
                match n with
                | ScalarNode scalar -> scalar.Value = value
                | _ -> false)
              |> Option.defaultValue false)

          match matchingIdx with
          | Some idx ->
            if List.isEmpty rest then
              s.Children.[idx] <- mergeFromYzl source s.Children.[idx]
            else
              navigateTo s.Children.[idx] rest
          | None -> failwithf "Sequence item with '%s=%s' not found" key value

        | SeqNode s, (None, Some(nullKey, value)) when nullKey = null ->
          let matchingIdx =
            s.Children
            |> Seq.tryFindIndex (fun child ->
              match child with
              | ScalarNode scalar when scalar.Value = value -> true
              | _ -> false)

          match matchingIdx with
          | Some idx ->
            if List.isEmpty rest then
              s.Children.[idx] <- mergeFromYzl source s.Children.[idx]
            else
              navigateTo s.Children.[idx] rest
          | None -> failwithf "Sequence item matching '%s' not found" value

        | _ -> failwithf "Path navigation failed: type mismatch at segment %A on node type %A" seg (node.GetType().Name)

    navigateTo target segments

  let loadFile (path: string) =
    use file = File.OpenRead path
    use reader = new StreamReader(file)
    let yaml = YamlStream()
    yaml.Load reader
    yaml

  let private serializer = SerializerBuilder().Build()

  let saveFile (path: string) (yaml: YamlStream) =
    use sw = new System.IO.StringWriter()
    serializer.Serialize(sw, yaml.Documents.[0].RootNode)

    let bytes =
      sw.ToString().Replace("\r\n", "\n") |> System.Text.Encoding.UTF8.GetBytes

    File.WriteAllBytes(path, bytes)

  let editInPlace (nodes: Node list) (filePath: string) =
    let stream = loadFile filePath

    for n in nodes do
      stream.Documents.[0].RootNode |> mergeFromYzl n |> ignore

    stream |> saveFile filePath


  let editInPlaceAtPath (node: Node) (jsonPath: string) (filePath: string) =
    let stream = loadFile filePath
    mergeFromYzlAtPath node stream.Documents.[0].RootNode jsonPath
    stream |> saveFile filePath

  let private removeNodeCore (doc: YamlDocument) (jsonPath: string) : bool =

    let rec navigate (node: YamlNode) segments : bool =
      match segments with
      | [] -> failwith "Empty path"
      | [ None, Some(predKey, predVal) ] ->
        match node with
        | :? YamlSequenceNode as sequence ->
          let idx =
            sequence.Children
            |> Seq.tryFindIndex (fun child ->
              match child with
              | :? YamlMappingNode as m when predKey <> null ->
                resolveNestedKey predKey (m :> YamlNode)
                |> Option.map (fun n ->
                  match n with
                  | ScalarNode scalar -> scalar.Value = predVal
                  | _ -> false)
                |> Option.defaultValue false
              | :? YamlScalarNode as s when predKey = null -> s.Value = predVal
              | _ -> false)

          match idx with
          | Some idx ->
            sequence.Children.RemoveAt idx
            true
          | None -> false
        | _ -> failwith "Predicate requires sequence"
      | (Some key, None) :: rest ->
        if List.isEmpty rest then
          match node with
          | :? YamlMappingNode as mapping -> mapping.Children.Remove(YamlScalarNode key)
          | :? YamlSequenceNode as sequence ->
            sequence.Children.RemoveAt(int key)
            true
          | _ -> failwithf "Cannot remove from %s" (node.GetType().Name)
        else
          let next =
            match node with
            | :? YamlMappingNode as mapping -> mapping.Children.[YamlScalarNode key]
            | :? YamlSequenceNode as sequence -> sequence.Children.[int key]
            | _ -> failwith "Invalid path"

          navigate next rest
      | (None, Some(predKey, predVal)) :: rest when predKey = null ->
        if List.isEmpty rest then
          match node with
          | :? YamlSequenceNode as sequence ->
            sequence.Children
            |> Seq.iter (fun c ->
              printfn
                "Child type: %s, value: %A"
                (c.GetType().Name)
                (match c with
                 | :? YamlScalarNode as s -> s.Value
                 | _ -> "not scalar"))

            let idx =
              sequence.Children
              |> Seq.tryFindIndex (fun child ->
                match child with
                | :? YamlScalarNode as s when s.Value = predVal -> true
                | _ -> false)

            match idx with
            | Some idx ->
              sequence.Children.RemoveAt idx
              true
            | None -> false
          | _ -> failwith "Scalar predicate requires sequence"
        else
          let next =
            match node with
            | :? YamlSequenceNode as sequence ->
              sequence.Children
              |> Seq.tryFind (fun child ->
                match child with
                | :? YamlScalarNode as s when s.Value = predVal -> true
                | _ -> false)
            | _ -> failwith "Scalar predicate requires sequence"

          match next with
          | Some next -> navigate next rest
          | None -> false

      | (None, Some(predKey, predVal)) :: rest ->
        let next =
          match node with
          | :? YamlSequenceNode as sequence ->
            sequence.Children
            |> Seq.tryFind (fun child ->
              match child with
              | :? YamlMappingNode as m ->
                resolveNestedKey predKey (m :> YamlNode)
                |> Option.map (fun n ->
                  match n with
                  | ScalarNode scalar -> scalar.Value = predVal
                  | _ -> false)
                |> Option.defaultValue false
              | _ -> false)
          | _ -> failwith "Predicate requires sequence"

        match next with
        | Some next -> navigate next rest
        | None -> false
      | (Some key, Some(predKey, predVal)) :: rest ->
        let sequence =
          match node with
          | :? YamlMappingNode as mapping -> mapping.Children.[YamlScalarNode key] :?> YamlSequenceNode
          | _ -> failwith "Expected mapping"

        let next =
          sequence.Children
          |> Seq.tryFind (fun child ->
            match child with
            | :? YamlMappingNode as m ->
              resolveNestedKey predKey (m :> YamlNode)
              |> Option.map (fun n ->
                match n with
                | ScalarNode scalar -> scalar.Value = predVal
                | _ -> false)
              |> Option.defaultValue false
            | _ -> false)

        match next with
        | Some next -> navigate next rest
        | None -> false
      | (None, None) :: _ -> failwithf "Unreachable"

    let segments = getSegments jsonPath
    let isRemoved = navigate doc.RootNode segments
    isRemoved

  let removeNodes (filePath: string) (jsonPaths: string list) : unit =
    let stream = loadFile filePath
    let doc = stream.Documents.[0]

    for jp in jsonPaths do
      removeNodeCore doc jp |> ignore

    saveFile filePath stream

  let removeNode (filePath: string) (jsonPath: string) : bool =
    let stream = loadFile filePath
    let doc = stream.Documents.[0]
    let isRemoved = removeNodeCore doc jsonPath
    saveFile filePath stream
    isRemoved
