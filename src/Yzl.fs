namespace Yzl.Core

open FSharp.Collections
open System.Text

[<RequireQualifiedAccessAttribute>]
module Yzl =
    
    type Str = 
     | Plain of string
     | SingleQuoted of string
     | DoubleQuoted of string
     | Folded of string
     | Literal of string

    type Name = | Name of string

    type Node =
      | Map of NamedNode list
      | Seq of Node list
      | Scalar of Scalar
      | None
      static member op_Implicit(source: int) :  Node = Scalar(Int source)
      static member op_Implicit(source: double) : Node = Scalar(Float source)
      static member op_Implicit(source: bool) : Node = Scalar(Bool source)
      static member op_Implicit(source: string) : Node = Scalar(Str (Plain source))
      static member op_Implicit(source: Node list) : Node = Seq(source)
      static member op_Implicit(source: NamedNode list) : Node = Map(source)
      static member op_Implicit(source: Node) : Node = source
      static member op_Implicit(source: NamedNode) : Node = Map([source]) 
    and NamedNode =
      | Named of Name * Node
    and Scalar =
      | Int of int
      | Float of double
      | Str of Str
      | Bool of bool

    /// Implicit cast helper
    let inline augment (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)

    let str t value = Named(Name (t), Scalar(Str(Plain value)))
    let int t value = Named(Name (t), Scalar(Int value))
    let float t value = Named(Name (t), Scalar(Float value))
    let boolean t value = Named(Name (t), Scalar(Bool value))
    let map t map = Named(Name (t), Map(map))
    let seq t seq = Named(Name(t), Seq(seq))
    

    type RenderOptions = { indentSpaces: int}
    let renderTree (yaml:Node) = sprintf "%A" yaml
    [<Literal>]
    let Zero = ""
    [<Literal>]
    let Space = " "
    [<Literal>]
    let Eol = "\n"

    let renderYaml (opts:RenderOptions) (yaml:Node) =
        let tab = System.String(' ', opts.indentSpaces)
        let builder = StringBuilder()

        let rec render (indent:string) this parent =

            let stringScalar = function
             | Plain z -> sprintf "%s\n" z
             | SingleQuoted z -> sprintf "'%s'\n" z
             | DoubleQuoted z -> sprintf "\"%s\"\n" z
             | Folded z -> sprintf "> \n%s\n" z
             | Literal z -> sprintf "| \n%s\n" z

            let stringify = function 
             | Int v -> Plain (v |> string)
             | Bool v -> Plain ((v |> string).ToLowerInvariant())
             | Float v -> Plain (v |> string)
             | Str s -> s

            let seqElem q = 
              let nextSeqIndent q = 
                indent +
                match q with
                 | Map _ -> tab
                 | Seq _ -> Eol + tab
                 | _ -> Zero

              builder.Append(sprintf "%s- " indent) |> ignore
              render  (nextSeqIndent q) q this

            let mapElem i m =
              let (Named (Name t, c)) = m
              let whitespace = function | Scalar _ -> Space | _ -> Eol
              let increment = function | Map _ -> tab | _ -> Zero
              let mapIndent = 
                match parent with
                 | Seq _ ->
                    match i with
                     | 0 -> Zero
                     |_ -> indent
                 | _ -> indent

              builder.Append(sprintf "%s%s:%s" mapIndent t (whitespace c)) |> ignore
              render  (indent + increment c) c this

            let r =
                match this with
                 | Scalar a -> 
                   builder.Append(a |> (stringify >> stringScalar)) |> ignore
                 | Seq qs -> qs |> Seq.iter seqElem
                 | Map ms -> ms |> Seq.iteri mapElem
                 | None -> ()
                 
            builder.Append(r) |> ignore
        render Zero yaml None
        builder.ToString()

    /// Renders using 2 spaces as indent
    let render yaml = renderYaml {indentSpaces=2} yaml 
