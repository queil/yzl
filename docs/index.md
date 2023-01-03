---
title: "Yzl - YAML Zero-Language - F# DSL for YAML"
---

[Yzl](https://github.com/queil/yzl) (pronounced *easel*) stands for YAML Zero-Language. It's an experimental pragmatic template-less approach to YAML generation powered by F#. It provides primitives (functions and operators) for string-less (almost) idiomatic YAML generation. It's an alternative to YAML templating engines. Yzl supports most common YAML features although it doesn't have an ambition to fully support the YAML specification. So if any feature is missing - open a GitHub issue.

## Getting started

1. Reference the [Yzl](https://www.nuget.org/packages/Yzl) NuGet package
2. Open `Yzl`
3. Start writing your YAML

## Examples

### Map

``` fsharp
open Yzl

let test = Yzl.boolean

test true |> Yzl.render
```

```yaml
test: true
```

### Sequence

``` fsharp
[ "a"; "b"; "c" ] |> Yzl.render
```

```yaml
- a
- b
- c
```

### Nesting

```fsharp
let rootMap = Yzl.map

rootMap [
  test false
] |> Yzl.render
```

```yaml
rootMap:
  test: false
```

### Ad-hoc map

```fsharp
[
  "number" .= 5
  "sequence" .= [
    938
    48
    1
  ]
  "otherKey" .= [
    "float" .= 1.56
  ]
] |> Yzl.render
```

```yaml
number: 5
sequence:
- 938
- 48
- 1
otherKey:
  float: 1.56
```

### Strings

Yzl supports different kinds of YAML strings via a [set of operators](reference/yzl-core-operators).
The convention here is that all the special kinds of strings are prefixed with `!`.

```f#
let myString = Yzl.str<Str>

[
  myString !|-
    """
    this is some
      special string
     with weird
        indentation
    """
] |> Yzl.render
```

```yaml
myString: |-
  this is some
    special string
   with weird
      indentation
```

### Dynamic generation

Yzl heavily exploits F# lists which means it is very easy to use list comprehension expressions to dynamically build YAML.

```fsharp
let map = [
  "one", 1
  "two", 2
  "three", 3
]

[
  for (k, v) in map do
    k .= v
] |> Yzl.render

```

```yaml
one: 1
two: 2
three: 3
```

### Heterogenous sequence

Creating a heterogenous sequence requires lifting values of different types to `Node` with the `!` custom lifting operator.

```fsharp
[
  ! "string"
  ! 1
  ! true
  ! 1.9
] |> Yzl.render

```

```yaml
- string
- 1
- true
- 1.9
```

### Keys names with characters invalid in F# identifiers

Sometimes YAML key names are not valid identifiers in F#. In such a case we can just use
standard F# escape characters ` `` `.

```fsharp
let ``app.kubernetes.io/component`` = Yzl.str<string>

[
  ``app.kubernetes.io/component`` "my-component"
] |> Yzl.render

```

```yaml
app.kubernetes.io/component: my-component

```

### Annotating types

Sometimes, especially when using Yzl.seq, you don't want to depend on the compiler to infer what type should be used. Or maybe you test things out in F# interactive where the compiler often lacks the context to infer the types you intended.

```fsharp
// this line on its own won't work in F# interactive
let mySeq = Yzl.seq

// this one will
let mySeq = Yzl.seq<int>

// also if you paste these two lines in one go it will work as the compiler has enough context to infer int
let mySeq2 = Yzl.seq
mySeq2 [1; 2; 3] |> Yzl.render

```

The same also applies to strings.

A node intended to use with plain strings:

```fsharp
let myPlainString = Yzl.str<string>
```

A node intended to be used with more complex strings via the string operators:

```fsharp
let description = Yzl.str<Str>
```

Automatic inference:

```fsharp
// make sure you copy/paste both lines in one go when using F# interactive

let mySnippet = Yzl.str

mySnippet !>-
  """
  hello world
    this is my snippet
    and it is great
  """
|> Yzl.render
```

### Overriding the key name inferred by the compiler

WARNING: this feature should only be used in case where the default key inferred by the compiler is incorrect or in generated code. Otherwise its usage is discouraged.

In the previous versions of Yzl the user had to explicitly pass the key name
as a string. The latest version uses Compiler Services to retrieve a calling member name
and use it as the key name. We can still emulate the old behaviour by passing the key name explicitly:

```fsharp
let myKey value = Yzl.map(value, "I want this to be my key name")
myKey [] |> Yzl.render
```

```yaml
I want this to be my key name: {}

```

### Bindings

It is an alpha feature where Yzl elements can be generated from a JSON schema so no manual map functions need to be created.

Available bindings:

* [kustomization](https://kustomize.io/) - [Yzl.Bindings.Kustomize](https://www.nuget.org/packages/Yzl.Bindings.Kustomize)

Additional bindings can be generated with Yzl.Bindings.Gen.
