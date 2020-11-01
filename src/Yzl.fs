/// <summary>
/// Yzl core modules
/// </summary>
namespace Yzl.Core

open FSharp.Collections
open System.Text
open System

[<RequireQualifiedAccessAttribute>]
module Yzl =
    
    /// YAML string types
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

    /// YAML node types
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

    /// Augments a given type to an Yzl node
    /// 
    /// *Possible augmentations are specified as implicit casts of the Node type*
    let inline augment (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)

    let private named t node = Named(Name t, node)
    
    /// Creates a named string scalar node
    let inline str name (node:^a) = Named(Name name, Scalar(Str (node |> augment)))
    
    /// Creates a named integer scalar node
    let int name value =  Scalar(Int value) |> named name
    
    /// Creates a named float scalar node
    let float name value = Scalar(Float value) |> named name
    
    /// Creates a named boolean scalar node
    let boolean name value = Scalar(Bool value) |> named name
    
    /// Creates a named map node
    let map name map =  MapNode(map) |> named name
    
    /// Creates a named sequence node
    let seq name seq =  SeqNode(seq) |> named name

    /// Creates an empty node
    /// 
    /// *Typically used when generating YAML tree conditionally to indicate no node should be generated*
    let none = Named(Name "", NoNode)

    /// YAML rendering options
    type RenderOptions = { 
      /// Specifies how many spaces are used as indentation in the output YAML
      indentSpaces: int 
    }

    [<Literal>]
    let private Empty = ""
    [<Literal>]
    let private Space = " "
    [<Literal>]
    let private Eol = "\n"

    /// Renders Yzl tree into YAML
    let renderYaml (opts:RenderOptions) (yaml:Node) =
        let tab = String(' ', opts.indentSpaces)
        let builder = StringBuilder()
        let append (s:string) = s |> builder.Append |> ignore

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
              let indentBase = lines |> Seq.head |> existingIndent

              System.String.Join(Eol,
                lines |> Seq.map (function
                     | s when s = Empty -> s
                     | s when String.IsNullOrWhiteSpace(s) -> requiredIndent
                     | s when (existingIndent s) = Empty -> requiredIndent + s
                     | s -> requiredIndent + s.[indentBase.Length..s.Length - 1]))
            
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

            let renderSeq qs =
              let seqElem q =
                let childIndent q =
                  indent +
                  match q with
                   | MapNode _ | SeqNode _ -> tab
                   | _ -> Empty

                match q with
                 | NoNode -> ()
                 | _ -> sprintf "%s- " indent |> append
                render (childIndent q) q this

              let maybeEol =
                match parent with
                 | SeqNode _ -> Eol
                 | _ -> Empty
              maybeEol |> append
              qs |> Seq.iter seqElem

            let renderMap ms =
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
              
                match (t, c) with
                 | Empty, NoNode -> ()
                 | _ -> sprintf "%s%s:%s" mapIndent t (whitespace c) |> append
                render (indent + increment c) c this

              ms |> Seq.iteri mapElem

            match this with
             | Scalar a -> a |> (stringify >> stringScalar) |> append
             | SeqNode qs -> qs |> renderSeq
             | MapNode ms -> ms |> renderMap
             | NoNode -> ()
                 
        render Empty yaml NoNode
        builder.ToString()

    /// Renders Yzl tree into YAML with the default RenderOptions
    let render yaml = renderYaml {indentSpaces=2} yaml 
