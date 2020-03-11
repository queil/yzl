module Yzl.Operators

open Yzl.Core

/// Implicit cast helper
let inline (!/@/) (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x)
/// Sequence element
let inline (!-) (n:^a) = !/@/n
let inline (!@) (f:Yzl.NamedNode seq -> Yzl.NamedNode) (x:Yzl.NamedNode seq) = !/@/(f x)
/// Folded string
let (!>) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Folded value))
/// Literal string
let (!|) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Literal value))
/// Untagged map
let inline (!%) (n:Yzl.NamedNode list) = Yzl.Map(n)
/// Untagged sequence
let inline (!--) n = Yzl.Seq(n)