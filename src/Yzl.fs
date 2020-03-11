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

    type Key = | Key of string

    type Node =
        | Map of KN list
        | Seq of Node list
        | Scalar of Scalar
        | None
        static member op_Implicit(source: System.Int32) :  Node = Scalar(Int(Plain source))
        static member op_Implicit(source: System.Double) : Node = Scalar(Float(Plain source))
        static member op_Implicit(source: System.Boolean) : Node = Scalar(Bool(Plain source))
        static member op_Implicit(source: System.String) : Node = Scalar(String(Plain source))
        static member op_Implicit(source: Node list) : Node = Seq(source)
       // static member op_Implicit(source: Node seq) : Node = Seq(source)
        static member op_Implicit(source: KN list) : Node = Map(source)
       // static member op_Implicit(source: KN seq) : Node = Map(source)
        static member op_Implicit(source: Node) : Node = source
        // static member op_Implicit(source: Tagged seq -> Tagged) : Tagged list -> Node = 
            
        //     fun z -> Map(z)
        static member op_Implicit(source: KN) : Node = Map([source]) 
            
    and KN =
        | KN of Key * Node
    and Scalar =
        | Int of Scalar<System.Int32>
        | Float of Scalar<System.Double>
        | String of Scalar<System.String>
        | Bool of Scalar<System.Boolean>

    let str t value = KN(Key (t), Scalar(String(Plain(value))))
    let int t value = KN(Key (t), Scalar(Int(Plain(value))))
    let float t value = KN(Key (t), Scalar(Float(Plain(value))))
    let boolean t value = KN(Key (t), Scalar(Bool(Plain(value))))
    let map t map = KN(Key (t), Map(map))
    let seq t seq = KN(Key(t), Seq(seq))

    type RenderOptions = { indentSpaces: int}
    let renderTree (yaml:Node) = sprintf "%A" yaml

    let renderYaml (opts:RenderOptions) (yaml:Node) =
        let tab = System.String(' ', opts.indentSpaces)
        let zero = ""
        let space = " "
        let eol = "\n"
        let pl1 = "||"
        let pl2 = "[["
        let pl3 = "''"
        let pl4 = ".."
        let pl5 = ",,"

        let rec render soFar (indent:string) this parent =

            let scalar = function
             | Plain z -> sprintf "%O\n" z
             | SingleQuoted z -> sprintf "'%O'\n" z
             | DoubleQuoted z -> sprintf "\"%O\"\n" z
             | Folded z -> sprintf "> \n%O\n" z
             | Literal z -> sprintf "| \n%O\n" z

            let nextMapIndent c = 
                indent +
                match c with
                 | Map _ -> tab
                 | Seq _ -> zero
                 | _ -> zero
            let nextSeqIndent q = 
                indent +
                match q with
                 | Map _ -> tab
                 | Seq _ -> pl4
                 | _ -> zero

            let seqElem q = 
                let ni = nextSeqIndent q
                render (sprintf "%s- " indent) ni q this

            let mapElem i m =
              let (KN (Key t, c)) = m
              let ni = nextMapIndent c
              let whitespace =
                match c with 
                 | Scalar _ -> space
                 | _ -> eol
              
              let indent = 
                match parent with
                 | Seq _ ->
                    match i,c with
                     | 0, _ -> zero
                     |_ -> indent
                 | _ -> indent
              render (sprintf "%s%s:%s" indent t whitespace) ni c this

            let r =
                match this with
                 | Scalar a -> 
                    match a with
                     | Int i -> scalar i
                     | Float f -> scalar f
                     | String b -> scalar b
                     | Bool v -> scalar v
                 | Seq qs -> qs |> Seq.map seqElem |> String.concat ""
                 | Map ms -> ms |> Seq.mapi mapElem |> String.concat ""
                 | None -> zero
                 
            soFar + r
        render "" "" yaml None
