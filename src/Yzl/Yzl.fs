namespace Yzl

/// <namespacedoc>
///   <summary>
///   Yzl core modules
///   </summary>
/// </namespacedoc>

open FSharp.Collections
open System.Text
open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<AutoOpen>]
module Core =

    /// [YAML string types](https://yaml-multiline.info/)
    type Str = 
     /// YAML plain string
     | Plain of string
     /// YAML single-quoted string
     | SingleQuoted of string
     /// YAML double-quoted string
     | DoubleQuoted of string
     /// YAML > string
     | Folded of string
     /// YAML >- string
     | FoldedStrip of string
     /// YAML >+ string
     | FoldedKeep of string
     /// YAML &#124; string
     | Literal of string
     /// YAML &#124;- string
     | LiteralStrip of string
     /// YAML &#124;+ string
     | LiteralKeep of string

     static member ToYzlString(source: Str) : Str = source
     static member ToYzlString(source: string) : Str = Plain source

    /// Represents the key in a YAML key-value pair
    type Name = | Name of name: string

    /// <summary>
    /// Lifts a given object to a <see cref="T:Node" />
    /// </summary>
    let inline lift (x:^a) : ^b = ((^a or ^b) : (static member ToYzl : ^a -> ^b) x)
    let inline liftString (x:^a) : ^b = ((^a or ^b) : (static member ToYzlString : ^a -> ^b) x)
    let inline liftMany (x:^a list) : ^b list = x |> List.map lift

    /// YAML node types
    type Node =
      /// YAML mapping
      | MapNode of NamedNode list
      /// YAML sequence
      | SeqNode of Node list
      /// YAML scalar
      | Scalar of Scalar
      /// Node with no representation
      | NoNode
      static member ToYzl(source: int) :  Node = Scalar(Int source)
      static member ToYzl(source: double) : Node = Scalar(Float source)
      static member ToYzl(source: bool) : Node = Scalar(Bool source)
      static member ToYzl(source: string) : Node = Scalar(Str (Plain source))
      static member ToYzl(source: Str) : Node = Scalar(Str (source))
      static member ToYzl(source: Node list) : Node = SeqNode(source)
      static member ToYzl(source: NamedNode list) : Node = MapNode(source)
      static member ToYzl(source: NamedNode list list) : Node = SeqNode(source |> List.map MapNode)
      static member ToYzl(source: int list) : Node = SeqNode( source |> List.map (Int >> Scalar))
      static member ToYzl(source: double list) : Node = SeqNode( source |> List.map (Float >> Scalar))
      static member ToYzl(source: bool list) : Node = SeqNode( source |> List.map (Bool >> Scalar))
      static member ToYzl(source: string list) : Node = SeqNode( source |> List.map (Plain >> Str >> Scalar))
      static member ToYzl(source: Node) : Node = source
      static member ToYzl(source: NamedNode) : Node = MapNode([source])
    /// YAML key-value pair
    and NamedNode =
      | Named of name: Name * node: Node
    /// YAML scalar types
    and Scalar =
      | Int of int
      | Float of double
      | Str of Str
      | Bool of bool
    and
      [<AbstractClass; Sealed>]
      Yzl =
        static member named (name:string) (node:Node) = Named(Name name, node)
        /// Creates a named string scalar node from F# string or Yzl.Str
        static member inline str< ^T when ( ^T or Str) : (static member ToYzlString:  ^T ->  Str) >(value: ^T, [<CallerMemberName;  Optional; DefaultParameterValue("")>] name: string) =
          let str : Str = liftString value 
          let yzl = lift str
          Yzl.named name yzl
        /// Creates a named integer scalar node
        static member int(value: int, [<CallerMemberName;  Optional; DefaultParameterValue("")>] name: string) =
          Scalar(Int value) |> Yzl.named name
        /// Creates a named float scalar node
        static member float(value: float, [<CallerMemberName;  Optional; DefaultParameterValue("")>] name: string) =
          Scalar(Float value) |> Yzl.named name
        /// Creates a named boolean scalar node
        static member boolean(value: bool, [<CallerMemberName;  Optional; DefaultParameterValue("")>] name: string) =
          Scalar(Bool value) |> Yzl.named name
        // Creates a named map node
        static member map(value: NamedNode list, [<CallerMemberName;  Optional; DefaultParameterValue("")>] name: string) =
          MapNode value |> Yzl.named name
        /// Creates a named sequence node
        static member inline seq< 'T when ( 'T or Node) : (static member ToYzl:  'T -> Node)> (value: 'T list, [<CallerMemberName;  Optional; DefaultParameterValue("")>]  name: string) =
          SeqNode(liftMany value) |> Yzl.named name
        /// Creates an empty node
        /// 
        /// *Typically used when generating YAML tree conditionally to indicate no node should be generated*
        static member none = Named(Name "", NoNode)

[<AutoOpen>]
module Render =
    
    open Core

    /// YAML rendering options
    type RenderOptions = { 
      /// Specifies how many spaces are used as indentation in the output YAML
      ///
      /// Default: `2`
      indentSpaces: int
      /// If set to true the indent of multi-line strings is calculated against the parent YAML node and excessive leading spaces get collapsed
      ///
      /// Default: `true`
      multiLineRelativeIndent: bool
    } with
       /// Default render options
       static member Default = { indentSpaces = 2; multiLineRelativeIndent = true }

    [<Literal>]
    let private Empty = ""
    [<Literal>]
    let private Space = " "
    [<Literal>]
    let private Eol = "\n"

    [<RequireQualifiedAccess>]
    module Yzl =
      /// Renders Yzl tree into YAML
      let renderYaml (opts:RenderOptions) (yaml:Node) =
          let tab = String(' ', opts.indentSpaces)
          let builder = StringBuilder()
          let append (s:string) = s |> builder.Append |> ignore

          let rec render (indent:string) this parent =

              let normalizeIndent (s: string) =
                if not opts.multiLineRelativeIndent then s
                else
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
                let existingIndent (z:string option) =
                  match z with | None -> Empty | Some z -> z.[0..z.IndexOf(z.TrimStart()) - 1]
                let indentBase = lines |> Seq.tryHead |> existingIndent

                System.String.Join(Eol,
                  lines |> Seq.map (function
                  | s when s = Empty -> s
                  | s when String.IsNullOrWhiteSpace(s) -> requiredIndent
                  | s when (existingIndent (Some s)) = Empty -> requiredIndent + s
                  | s -> requiredIndent + s.[indentBase.Length..s.Length - 1]))

              let nullOr = function | null -> "null" | z -> z

              let stringScalar = function
              | Plain z -> sprintf "%s\n" (nullOr z)
              | SingleQuoted z -> sprintf "'%s'\n" (nullOr z)
              | DoubleQuoted z -> sprintf "\"%s\"\n" (nullOr z)
              | Folded z -> sprintf ">\n%s\n" (nullOr z |> normalizeIndent)
              | FoldedStrip z -> sprintf ">-\n%s\n" (nullOr z |> normalizeIndent)
              | FoldedKeep z -> sprintf ">+\n%s\n" (nullOr z |> normalizeIndent)
              | Literal z -> sprintf "|\n%s\n" (nullOr z |> normalizeIndent)
              | LiteralStrip z -> sprintf "|-\n%s\n" (nullOr z |> normalizeIndent)
              | LiteralKeep z -> sprintf "|+\n%s\n" (nullOr z |> normalizeIndent)

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
                match qs with
                | [] -> "[]\n" |> append
                | qs' -> qs' |> Seq.iter seqElem

              let renderMap ms =
                let mapElem i m =
                  let (Named (Name t, c)) = m
                  let eolOrSpace = function | Scalar _ -> Space | MapNode [] -> Space | SeqNode [] -> Space | _ -> Eol
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
                  | _ -> sprintf "%s%s:%s" mapIndent t (eolOrSpace c) |> append
                  render (indent + increment c) c this

                match ms with
                | [] -> "{}\n" |> append
                | ms' -> ms' |> Seq.iteri mapElem

              match this with
              | Scalar a -> a |> (stringify >> stringScalar) |> append
              | SeqNode qs -> qs |> renderSeq
              | MapNode ms -> ms |> renderMap
              | NoNode -> ()

          render Empty yaml NoNode
          builder.ToString()

      /// <summary>Renders Yzl tree into YAML with the default RenderOptions</summary>
      /// <example>
      /// Render an Yzl node: `! 5 |> Yzl.render`
      /// </example>
      let inline render (obj:^a) = renderYaml RenderOptions.Default (lift obj)
