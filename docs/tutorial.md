# Tutorial

You can play with Yzl in `dotnet fsi`.

Run `dotnet fsi` and reference Yzl NuGet and open the `Yzl.Core` namespace:
```fsharp
#r "nuget: Yzl"; open Yzl.Core;;
```

## The augmentation operator

Yzl uses `!` as an operator that augments (or elevates) simple F# types to `Yzl.Node`

## Scalar

### Yaml
```yaml
5
```

### Yzl
```fsharp
! 5 |> Yzl.render
```

## Homogenous sequence

### YAML

```yaml
- 1
- 2
- 3
```

### Yzl

Common F# lists are rendered as sequences.

```fsharp
! [1; 2; 3] |> Yzl.render
```

## Heterogenous sequence

### YAML

```yaml
- true
- 2.0
- three
```

### Yzl

To render a heterogenous sequence every element must be preceded by the augmentation operator (`!`)

```fsharp
! [ ! true; ! 2.0; ! "three"] |> Yzl.render
```

## Named value (key-value pair)

### YAML

```yaml
language: F#
```

### Yzl

```fsharp
let language (x:string) = Yzl.str "language" x;;
! (language "F#" ) |> Yzl.render;;
```

In a non-interactive context the first line of the above can be simplified to:

```fsharp
let language = Yzl.str "language"
```

