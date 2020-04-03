# yzl [![Build Status](https://dev.azure.com/queil/yzl/_apis/build/status/queil.yzl?branchName=master)](https://dev.azure.com/queil/yzl/_build/latest?definitionId=2&branchName=master) [![NuGet Badge](https://buildstats.info/nuget/Yzl?includePreReleases=true)](https://www.nuget.org/packages/Yzl)

yzl /ˈiːz(ə)l/ - YAML Zero-Language - F# DSL for YAML

Yzl is a DSL alternative for templating YAML. It was inspired by [Suave HTML View Engine](https://github.com/SuaveIO/suave/blob/master/src/Suave.Experimental/Html.fs).

## Example

Yzl:

```fsharp
! [
    name "Martin D'vloper"
    job "Developer"
    skill "Elite"
    employed true
    foods [
      ! "Apple"
      ! "Orange"
      ! "Strawberry"
      ! "Mango" ]
    languages [
      perl "Elite"
      python "Elite"
      pascal "Lame" ]
    education !|
      """
      4 GCSEs
      3 A-Levels
      BSc in the Internet of Things
      """ 
]
```

Yaml:

```yaml
name: Martin D'vloper
job: Developer
skill: Elite
employed: true
foods:
- Apple
- Orange
- Strawberry
- Mango
languages:
  perl: Elite
  python: Elite
  pascal: Lame
education: |
  4 GCSEs
  3 A-Levels
  BSc in the Internet of Things
```
