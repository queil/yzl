namespace Yzl.Core

open FSharp.Collections
open System.Text
open System

[<RequireQualifiedAccessAttribute>]
module Yzl =
    
    type Str = 
     | Plain of string
     | SingleQuoted of string
     | DoubleQuoted of string
     | Folded of string
     | FoldedDash of string
     | Literal of string
     | LiteralDash of string
     static member op_Implicit(source: string) : Str = Plain source
     static member op_Implicit(source: Str) : Str = source

    type Name = | Name of string

    type Node =
      | MapNode of NamedNode list
      | SeqNode of Node list
      | Scalar of Scalar
      | NoNode
      static member op_Implicit(source: int) :  Node = Scalar(Int source)
      static member op_Implicit(source: double) : Node = Scalar(Float source)
      static member op_Implicit(source: bool) : Node = Scalar(Bool source)
      static member op_Implicit(source: string) : Node = Scalar(Str (Plain source))
      static member op_Implicit(source: Node list) : Node = SeqNode(source)
      static member op_Implicit(source: NamedNode list) : Node = MapNode(source)
      static member op_Implicit(source: Node) : Node = source
      static member op_Implicit(source: NamedNode) : Node = MapNode([source]) 
    and NamedNode =
      | Named of Name * Node
    and Scalar =
      | Int of int
      | Float of double
      | Str of Str
      | Bool of bool

    /// Implicit cast helper
    let inline augment (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)

    let private named t node = Named(Name t, node)
    let inline str t (node:^a) = Named(Name t, Scalar(Str (node |> augment)))
    let int name value =  Scalar(Int value) |> named name
    let float name value = Scalar(Float value) |> named name
    let boolean name value = Scalar(Bool value) |> named name
    let map name map =  MapNode(map) |> named name
    let seq name seq =  SeqNode(seq) |> named name
    
    type RenderOptions = { indentSpaces: int}
    let renderTree (yaml:Node) = sprintf "%A" yaml
    [<Literal>]
    let Empty = ""
    [<Literal>]
    let Space = " "
    [<Literal>]
    let Eol = "\n"

    let renderYaml (opts:RenderOptions) (yaml:Node) =
        let tab = String(' ', opts.indentSpaces)
        let builder = StringBuilder()

        let rec render (indent:string) this parent =

            let normalizeIndent (s: string) =
              let lines = 
                let fixFirst z =
                  match z with
                   | [] -> []
                   | h::t when String.IsNullOrWhiteSpace(h) -> t
                   | _ -> z
                let fixLast z =
                  match z with
                   | [] -> []
                   | ls when String.IsNullOrWhiteSpace(ls |> List.last) -> ls.[0..ls.Length - 2]
                   | _ -> z
                s.Split('\n') |> Seq.toList |> fixFirst |> fixLast
              let requiredIndent = indent + tab
              let existingIndent (z:string) = z.[0..z.IndexOf(z.TrimStart()) - 1]
              let indentBase = lines |> Seq.pick (fun x ->
                let exs = (existingIndent x)
                if exs <> Empty then Some exs else Some "")

              System.String.Join(Eol,
                lines |> Seq.map (fun x ->
                    match x with
                     | s when s = Empty -> s
                     | s when String.IsNullOrWhiteSpace(s) -> requiredIndent
                     | s when (existingIndent s) = Empty -> requiredIndent + s
                     | _ -> requiredIndent + x.[indentBase.Length..x.Length - 1]
                   )
                 )
            
            let nullOr = function | null -> "null" | z -> z
                 
            let stringScalar = function
             | Plain z -> sprintf "%s\n" (nullOr z)
             | SingleQuoted z -> sprintf "'%s'\n" (nullOr z)
             | DoubleQuoted z -> sprintf "\"%s\"\n" (nullOr z)
             | Folded z -> sprintf ">\n%s\n" (nullOr z |> normalizeIndent)
             | FoldedDash z -> sprintf ">-\n%s\n" (nullOr z |> normalizeIndent)
             | Literal z -> sprintf "|\n%s\n" (nullOr z |> normalizeIndent)
             | LiteralDash z -> sprintf "|-\n%s\n" (nullOr z |> normalizeIndent)

            let stringify = function 
             | Int v -> Plain (v |> string)
             | Bool v -> Plain ((v |> string).ToLowerInvariant())
             | Float v -> Plain (v |> string)
             | Str s -> s

            let seqElem q = 
              let nextSeqIndent q = 
                indent +
                match q with
                 | MapNode _ -> tab
                 | SeqNode _ -> Eol + tab
                 | _ -> Empty

              builder.Append(sprintf "%s- " indent) |> ignore
              render  (nextSeqIndent q) q this

            let mapElem i m =
              let (Named (Name t, c)) = m
              let whitespace = function | Scalar _ -> Space | _ -> Eol
              let increment = function | MapNode _ -> tab | _ -> Empty
              let mapIndent = 
                match parent with
                 | SeqNode _ ->
                    match i with
                     | 0 -> Empty
                     |_ -> indent
                 | _ -> indent

              builder.Append(sprintf "%s%s:%s" mapIndent t (whitespace c)) |> ignore
              render  (indent + increment c) c this

            let r =
                match this with
                 | Scalar a -> 
                   builder.Append(a |> (stringify >> stringScalar)) |> ignore
                 | SeqNode qs -> qs |> Seq.iter seqElem
                 | MapNode ms -> ms |> Seq.iteri mapElem
                 | NoNode -> ()
                 
            builder.Append(r) |> ignore
        render Empty yaml NoNode
        builder.ToString()

    /// Renders using 2 spaces as indent
    let render yaml = renderYaml {indentSpaces=2} yaml 
