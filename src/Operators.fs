module Yzl.Operators

open Yzl.Core

/// Augment to Yzl Node
let inline (!) (a:^a) = Yzl.augment a
/// Folded string
let (!>) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Folded value))
/// Literal string
let (!|) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Literal value))

