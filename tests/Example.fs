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

    let name = Yzl.str "name"
    let job = Yzl.str "job"
    let skill = Yzl.str "skill"
    let employed = Yzl.boolean "employed"
    let foods = Yzl.liftMany >> Yzl.seq "foods"
    let languages = Yzl.map "languages"
    let education = Yzl.strYaml "education"

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
              "Apple"
              "Orange"
              "Strawberry"
              "Mango" ]
            languages [
              "perl" .= "Elite"
              "python" .= "Elite"
              "pascal" .= "Lame"]
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
