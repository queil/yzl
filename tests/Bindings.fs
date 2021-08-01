namespace Yzl.Tests.Unit

module Bindings =

  open Expecto
  open Yzl.Core
  open Yzl.Bindings.Kustomize

    
    

    
    
    

  [<Tests>]
  let tests =
    testList "Bindings" [
      
      test "Kustomize" {
        
        let expected = ""
        "Rendering failed" |> Expect.equal (!5 |> Yzl.render) expected  
      }
    ]
