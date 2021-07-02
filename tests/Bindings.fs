namespace Yzl.Tests.Unit

module Bindings =

  open Expecto
  open Yzl.Core
  

  [<AutoOpen>]
  module Test2 =
    
    

    
    type Kustz(?images: Kustz, ?other: int) = 
      member x.Images = images
    and
     Kustomization =
     private
     | ApiVersion of ApiVersion<Kustomization>
     | Images of Image seq
    and ApiVersion<'a> = 'a option -> string
    and Image =
     | Name of Name<Image>
     | ApiVersion of ApiVersion<Image>
    and Name<'a> = 'a option -> string
 
    let apiVersion (value:ApiVersion<'a>) = 
      fun z ->
        let x = value None
        Yzl.str "apiVersion" x
   
    let images (x: (list<(Image option -> Yzl.NamedNode)>)) = 
      x |> List.map (fun z -> z None)
    
    

  [<Tests>]
  let tests =
    testList "Bindings" [
      
      test "Kustomize" {
        let kustomization (x: (list<(Kustomization option -> Yzl.NamedNode)>)) = 
          ! ((x |> List.map (fun z -> z None ) ))
        
        let ii = ! 2
        let y = ii := "test"
           
        
        
        let yaml = 
          kustomization [
            apiVersion (fun x -> "test")
            // images [
            //   ! [
            //     fun x -> Yzl.map "image" []
            //   ]
            // ]
            
            ]
        let expected = ""
        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected  
      }
    ]
