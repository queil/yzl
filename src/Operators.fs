namespace Yzl.Core

[<AutoOpen>]
module Operators =

  /// Augment to Yzl Node
  let inline (!) (a:^a) = Yzl.augment a
  /// Folded string
  let (!>) (value:string) = Yzl.Folded value
  /// Folded dash
  let (!>-) (value:string) = Yzl.FoldedDash value
  /// Literal string
  let (!|) (value:string) = Yzl.Literal value
  /// Literal dash string
  let (!|-) (value:string) = Yzl.LiteralDash value
  