namespace Yzl

[<AutoOpen>]
module Operators =

  open Core
  /// Lift to Yzl Node
  let inline (!) (a:^a) : Node = lift a
  /// Operator for ad-hoc maps creation.
  let inline (.=) (name: string) (value:^a) : NamedNode = Yzl.named(lift value, name)
  /// Folded string
  let (!>) (value:string) = Folded value
  /// Folded strip string
  let (!>-) (value:string) = FoldedStrip value
  /// Folded keep string
  let (!>+) (value:string) = FoldedKeep value
  /// Literal string
  let (!|) (value:string) = Literal value
  /// Literal strip string
  let (!|-) (value:string) = LiteralStrip value
  /// Literal keep string
  let (!|+) (value:string) = LiteralKeep value
