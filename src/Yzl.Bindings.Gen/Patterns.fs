namespace Yzl.Bindings.Gen
open NJsonSchema

module Patterns =

  let matchNonEmpty =
    function
     | null -> None
     | defs ->
       match defs |> Seq.toList |> List.map (|KeyValue|) with
       | [] -> None
       | xs -> Some xs

  let matchType (typ:JsonObjectType) (x:JsonSchema) =
    match x with
    | x when x.Type = typ -> Some x
    | _ -> None 

  let (|Properties|_|) (s:JsonSchema) =
    s.Properties |> matchNonEmpty

  let (|PatternProperties|_|) (s:JsonSchema) =
    s.PatternProperties |> matchNonEmpty

  let (|Array2|_|) (s:JsonSchema) =
    match s.Items |> Seq.toList with
    | [] -> None
    | xs -> Some xs

  let (|OneOf|_|) (s:JsonSchema) =
    match s.OneOf |> Seq.toList with
    | [] -> None
    | xs -> Some xs

  let (|Array|_|) (s:JsonSchema) =
    match s.Item with
    | null -> None
    | v -> Some v

  let (|Enum|_|) (s:JsonSchema) =
    match s.Enumeration |> Seq.toList with
    | [] -> None
    | v -> Some (v, s)

  let (|String|_|) (s:JsonSchema) =
    s |> matchType JsonObjectType.String

  let (|Integer|_|) (s:JsonSchema) =
    s |> matchType JsonObjectType.Integer

  let (|Number|_|) (s:JsonSchema) =
    s |> matchType JsonObjectType.Number
  
  let (|Boolean|_|) (s:JsonSchema) =
    s |> matchType JsonObjectType.Boolean

  let (|Reference|_|) (s:JsonSchema) =
    match s with
    | x when x.Reference |> isNull |> not -> Some x.Reference
    | _ -> None

  let (|Object|_|) (s:JsonSchema) =
    match s with
    | x when x.IsObject -> Some x
    | _ -> None

  let (|Definitions|_|) (s:JsonSchema) =
    s.Definitions |> matchNonEmpty
