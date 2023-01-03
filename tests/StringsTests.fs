namespace Yzl.Tests.Unit

module Strings =

  open Expecto
  open Yzl
  open System.IO


  let myText = Yzl.str

  let multistring = Yzl.seq

  [<Tests>]
  let tests =

    testList "generate" [
      
      test "Literal dash" {
        let expected = File.ReadAllText("./yaml/literal-dash.yaml")
        let yaml = ![
          "parent" .= [
             myText !|-
  // START: this space is left here purposely - do not remove
                                             """
                                             some
                                             - text should
                                             - have 
                                                 the indentation adjusted to the parent node
                                             """
  // END: this space is left here purposely - do not remove
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }

      test "Literal" {
        let expected = File.ReadAllText("./yaml/literal.yaml")

        let yaml = [
          "parent" .= [
           myText !|
  // START: this space is left here purposely - do not remove
               """
               some
               - text should : everything is allowed in here
               other-things 
                   - the indentation adjusted to the parent node
                   - 1
               """
  // END: this space is left here purposely - do not remove
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }

      test "Folded dash" {
        let expected = File.ReadAllText("./yaml/folded-dash.yaml")
        let yaml = [
          "parent" .= [
           myText !>-
  // START: this space is left here purposely - do not remove
                                             """
                                             rg             
                                               szf    
                                                 szgszdafsesf
                                               awe             
                                             ffe        
                                             """
  // END: this space is left here purposely - do not remove
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }

      test "Folded" {
        let expected = File.ReadAllText("./yaml/folded.yaml")

        let yaml = [
          "parent" .= [
           myText !>
  // START: this space is left here purposely - do not remove
                  """
                  lorem ipsum      
                  does not make that     
                      much of a sense i   
                                    ------- ----- 33nw    
                      e  
                  3r m4qlf853


                  
                  """
  // END: this space is left here purposely - do not remove
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }

      test "Folded null" {
        let expected = File.ReadAllText("./yaml/folded-null.yaml")

        let yaml = [
          "parent" .= [
            myText !> null
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }
      
      test "Mixed-string seq" {
        let expected = File.ReadAllText("./yaml/multi-string-seq.yaml")
         

        let yaml = [
           multistring [
           ! "plain"
           ! !> "
              folded
                lorem ipsum"
           ! !>- "
              folded strip
                lorem ipsum"
           ! !>+ "
              folded keep
                lorem ipsum"
           ! !| "
              literal
                lorem ipsum"
           ! !|- "
              literal strip
                lorem ipsum"
           ! !|+ "
              literal keep
                lorem ipsum"
           ! "\"double-quoted\""
           ! "'single-quoted'"
          ]
        ]
        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }
      
      test "Empty string multiline" {
        let expected = "test: |

"
        let yaml = [
          "test" .= !| ""
        ]
        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected
      }

      //TODO: add null strings tests
    ]
