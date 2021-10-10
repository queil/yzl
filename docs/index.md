---
title: "Yzl - YAML Zero-Language - F# DSL for YAML"
---

[Yzl](https://github.com/queil/yzl) (pronounced *easel*) stands for YAML Zero-Language. It's an experimental pragmatic template-less approach to YAML generation powered by F#. It provides primitives (functions and operators) for string-less (almost) idiomatic YAML generation. It's an alternative to YAML templating engines. Yzl supports most common YAML features although it doesn't have an ambition to fully support the YAML specification. So if any feature is missing - open a GitHub issue.

## Getting started

1. Reference the [Yzl](https://www.nuget.org/packages/Yzl) NuGet package
2. Open `Yzl.Core`
3. Start writing your YAML

## Examples

#### Map

``` fsharp
open Yzl.Core

let test = Yzl.boolean "test"

[ test true ] |> Yzl.render
```

```yaml
test: true
```

#### Sequence

``` fsharp
[ "a"; "b"; "c" ] |> Yzl.render
```

```yaml
- a
- b
- c
```

#### Nesting

```fsharp
let rootMap = Yzl.map "rootMap"
[
  rootMap [
    test false
  ]
] |> Yzl.render
```

```yaml
rootMap:
  test: false
```

#### Ad-hoc map

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

#### Strings

Yzl supports different kinds of YAML strings via a [set of operators](reference/yzl-core-operators).
The convention here is that all the special kinds of strings are prefixed with `!`.

```f#
let myString = Yzl.strYaml "myString"

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

#### Dynamic generation

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

#### Bindings

It is an alpha feature where Yzl elements can be generated from a JSON schema so no manual map functions need to be created.

Available bindings:

* [kustomization](https://kustomize.io/) - [Yzl.Bindings.Kustomize](https://www.nuget.org/packages/Yzl.Bindings.Kustomize)

Additional bindings can be generated with Yzl.Bindings.Gen.
