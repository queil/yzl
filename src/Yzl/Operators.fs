namespace Yzl.Core

[<AutoOpen>]
module Operators =

  /// Lift to Yzl Node
  let inline (!) (a:^a) : Yzl.Node = Yzl.lift a
  /// Lift to Yzl Node list
  let inline (!!) (a:^a list) : Yzl.Node list = Yzl.liftMany a
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
