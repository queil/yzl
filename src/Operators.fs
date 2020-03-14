namespace Yzl.Core

[<AutoOpen>]
module Operators =

  /// Augment to Yzl Node
  let inline (!) (a:^a) = Yzl.augment a
  /// Folded string
  let (!>) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Folded value))
  /// Literal string
  let (!|) (value:string) = Yzl.Scalar(Yzl.Str(Yzl.Literal value))

