namespace Yzl.Core

/// <namespacedoc>
///   <summary>
///   Yzl core modules
///   </summary>
/// </namespacedoc>

open FSharp.Collections
open System.Text
open System

[<RequireQualifiedAccessAttribute>]
module Yzl =
    
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

     static member op_Implicit(source: string) : Str = Plain source
     static member op_Implicit(source: Str) : Str = source
    
    /// Represents the key in a YAML key-value pair
    type Name = | Name of name: string

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
      static member op_Implicit(source: int) :  Node = Scalar(Int source)
      static member op_Implicit(source: double) : Node = Scalar(Float source)
      static member op_Implicit(source: bool) : Node = Scalar(Bool source)
      static member op_Implicit(source: string) : Node = Scalar(Str (Plain source))
      static member op_Implicit(source: Str) : Node = Scalar(Str (source))
      static member op_Implicit(source: Node list) : Node = SeqNode(source)
      static member op_Implicit(source: NamedNode list) : Node = MapNode(source)
      static member op_Implicit(source: NamedNode list list) : Node = SeqNode(source |> List.map MapNode)
      static member op_Implicit(source: int list) : Node = SeqNode( source |> List.map (Int >> Scalar))
      static member op_Implicit(source: double list) : Node = SeqNode( source |> List.map (Float >> Scalar))
      static member op_Implicit(source: bool list) : Node = SeqNode( source |> List.map (Bool >> Scalar))
      static member op_Implicit(source: string list) : Node = SeqNode( source |> List.map (Plain >> Str >> Scalar))
      static member op_Implicit(source: Node) : Node = source
      static member op_Implicit(source: NamedNode) : Node = MapNode([source])
    /// YAML key-value pair
    and NamedNode =
      | Named of name: Name * node: Node
    /// YAML scalar types
    and Scalar =
      | Int of int
      | Float of double
      | Str of Str
      | Bool of bool
    and Builder() =
      static member named (name:string) (node:Node) = Named(Name name, node)
      /// Creates a named string scalar node from F# string
      static member str (value:string) : string -> NamedNode = fun name -> Named(Name name, Scalar(Str (Plain value)))
      /// Creates a named string scalar node from Str
      static member str (value:Str) = fun name -> Named(Name name, Scalar(Str value))
      /// Creates a named integer scalar node
      static member int (value:int) = fun name -> Scalar(Int value) |> Builder.named name
      /// Creates a named float scalar node
      static member float (value:float) = fun name -> Scalar(Float value) |> Builder.named name
      /// Creates a named boolean scalar node
      static member boolean (value:bool) = fun name -> Scalar(Bool value) |> Builder.named name
      /// Creates a named map node
      static member map (value:NamedNode list) = fun name -> MapNode(value) |> Builder.named name
      /// Creates a named sequence node
      static member seq (seq: Node list) =  fun name -> SeqNode(seq) |> Builder.named name
      /// Creates an empty node
      /// 
      /// *Typically used when generating YAML tree conditionally to indicate no node should be generated*
      static member none = Named(Name "", NoNode)

    /// <summary>
    /// Lifts a given object to a <see cref="T:Node" />
    /// </summary>
    /// <remarks>
    /// *Possible mappings are specified as implicit casts of the <see cref="T:Node" /> type*
    /// </remarks>
    let inline lift (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)
    [<Obsolete("Use lift instead")>]
    let inline augment (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)
    let inline liftMany (x:^a list) : ^b list = x |> List.map lift

    let private named t node = Named(Name t, node)
    
    /// Creates a named string scalar node
    [<Obsolete("Use Yzl.Builder.str")>]
    let inline str name (node:^a) = Named(Name name, Scalar(Str (node |> lift)))
    
    /// Creates a named integer scalar node
    [<Obsolete("Use Yzl.Builder.int")>]
    let int name value =  Scalar(Int value) |> named name
    
    /// Creates a named float scalar node
    [<Obsolete("Use Yzl.Builder.float")>]
    let float name value = Scalar(Float value) |> named name
    
    /// Creates a named boolean scalar node
    [<Obsolete("Use Yzl.Builder.boolean")>]
    let boolean name value = Scalar(Bool value) |> named name
    
    /// Creates a named map node
    [<Obsolete("Use Yzl.Builder.map")>]
    let map name map =  MapNode(map) |> named name
    
    /// Creates a named sequence node
    [<Obsolete("Use Yzl.Builder.seq")>]
    let seq name seq =  SeqNode(seq) |> named name

    /// Creates an empty node
    /// 
    /// *Typically used when generating YAML tree conditionally to indicate no node should be generated*
    [<Obsolete("Use Yzl.Builder.none")>]
    let none = Named(Name "", NoNode)

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
                let eolOrSpace = function | Scalar _ -> Space | SeqNode [] -> Space | _ -> Eol
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

              ms |> Seq.iteri mapElem

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
