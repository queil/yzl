namespace Yzl.Core

open FSharp.Collections

[<RequireQualifiedAccessAttribute>]
module Yzl =
    type Scalar<'a> = 
        | Plain of 'a
        | SingleQuoted of 'a
        | DoubleQuoted of 'a
        | Folded of 'a
        | Literal of 'a

    type Name = | Name of string

    type Node =
        | Map of NamedNode list
        | Seq of Node list
        | Scalar of Scalar
        | None
        static member op_Implicit(source: int) :  Node = Scalar(Int(Plain source))
        static member op_Implicit(source: double) : Node = Scalar(Float(Plain source))
        static member op_Implicit(source: bool) : Node = Scalar(Bool(Plain source))
        static member op_Implicit(source: string) : Node = Scalar(Str(Plain source))
        static member op_Implicit(source: Node list) : Node = Seq(source)
        static member op_Implicit(source: NamedNode list) : Node = Map(source)
        static member op_Implicit(source: Node) : Node = source
        static member op_Implicit(source: NamedNode) : Node = Map([source]) 
    and NamedNode =
        | Named of Name * Node
    and Scalar =
        | Int of Scalar<int>
        | Float of Scalar<double>
        | Str of Scalar<string>
        | Bool of Scalar<bool>

    let str t value = Named(Name (t), Scalar(Str(Plain(value))))
    let int t value = Named(Name (t), Scalar(Int(Plain(value))))
    let float t value = Named(Name (t), Scalar(Float(Plain(value))))
    let boolean t value = Named(Name (t), Scalar(Bool(Plain(value))))
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

        let rec render soFar (indent:string) this parent =

            let scalar = function
             | Plain z -> sprintf "%O\n" z
             | SingleQuoted z -> sprintf "'%O'\n" z
             | DoubleQuoted z -> sprintf "\"%O\"\n" z
             | Folded z -> sprintf "> \n%O\n" z
             | Literal z -> sprintf "| \n%O\n" z

            let seqElem q = render (sprintf "%s- " indent) (indent + tab) q this

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
 
              render (sprintf "%s%s:%s" mapIndent t (whitespace c)) (indent + increment c) c this

            let r =
                match this with
                 | Scalar a -> 
                    match a with 
                     | Int i -> scalar i
                     | Float f -> scalar f
                     | Str b -> scalar b
                     | Bool v -> scalar v
                 | Seq qs -> qs |> Seq.map seqElem |> String.concat Zero
                 | Map ms -> ms |> Seq.mapi mapElem |> String.concat Zero
                 | None -> Zero
                 
            soFar + r
        render "" "" yaml None
