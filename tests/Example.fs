namespace Yzl.Tests.Unit

module Example =

  open Expecto
  open Yzl
  open System.IO

  [<AutoOpen>]
  module Fields =

    type LanguageLevel =
     | Elite
     | Lame

    let elite = Elite
    let lame = Lame

    let name = Yzl.str
    let job = Yzl.str
    let skill = Yzl.str
    let employed = Yzl.boolean
    let foods = Yzl.seq
    let languages = Yzl.map
    let education = Yzl.str

  [<Tests>]
  let tests =
    testList "Generate" [
      
      test "Example" {
        let expected = File.ReadAllText("./yaml/example.yaml")
        let yaml = [
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
