namespace Yzl.Tests.Unit

module Example =

  open Expecto
  open Yzl.Core
  open System.IO

  [<AutoOpen>]
  module Fields =

    type LanguageLevel =
     | Elite
     | Lame

    let elite = Elite
    let lame = Lame

    type Language =
     | Python of level:LanguageLevel
     | Perl of level:LanguageLevel
     | Pascal of level:LanguageLevel
     with member x.Deconstruct = 
                 match x with
                 | Python z -> ("python", z |> string)
                 | Pascal z -> ("pascal", z |> string)
                 | Perl z -> ("perl", z |> string)
    let perl = Perl
    let python = Python
    let pascal = Pascal

    let name (x:string) = Yzl.Builder.str x "name"
    let job (x:string)= Yzl.Builder.str x "job"
    let skill (x:string)= Yzl.Builder.str x "skill"
    let employed x = Yzl.Builder.boolean x "employed"
    let foods x = Yzl.Builder.seq x "foods"
    let languages (xs:Language list) = "languages" |> Yzl.Builder.map (xs |> List.map (fun x -> x.Deconstruct) |> List.map(fun (k,v) -> Yzl.str k v))
    let education (x:Yzl.Str) = Yzl.Builder.str x "education"

  [<Tests>]
  let tests =
    testList "Generate" [
      
      test "Example" {
        let expected = File.ReadAllText("./yaml/example.yaml")
        let yaml = ! [
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
              perl elite
              python elite
              pascal lame ]
            education !|
              """
              4 GCSEs
              3 A-Levels
              BSc in the Internet of Things
              """
          ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }
    ]
