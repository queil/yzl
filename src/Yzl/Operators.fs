namespace Yzl.Core

[<AutoOpen>]
module Operators =

  /// Augment to Yzl Node
  let inline (!) (a:^a) : Yzl.Node = Yzl.augment a
  /// Folded string
  let (!>) (value:string) = Yzl.Folded value
  /// Folded strip string
  let (!>-) (value:string) = Yzl.FoldedStrip value
  /// Folded keep string
  let (!>+) (value:string) = Yzl.FoldedKeep value
  /// Literal string
  let (!|) (value:string) = Yzl.Literal value
  /// Literal strip string
  let (!|-) (value:string) = Yzl.LiteralStrip value
  /// Literal keep string
  let (!|+) (value:string) = Yzl.LiteralKeep value
