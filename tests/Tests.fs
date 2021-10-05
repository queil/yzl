namespace Yzl.Tests.Unit

module Core =

  open Expecto
  open Yzl.Core
  open System.IO

  let kind (value:string) = Yzl.Builder.str value "kind"
  let apiVersion (value:string) = Yzl.Builder.str value "apiVersion"
  let metadata x = Yzl.Builder.map x "metadata"
  let name (value:string) = Yzl.Builder.str value "name"
  let value (str:string) = Yzl.Builder.str str "value"
  let labels x = Yzl.Builder.map x "labels"
  let app (value:string) = Yzl.Builder.str value "app"
  let spec x = Yzl.Builder.map x "spec"
  let replicas x = Yzl.Builder.int x "replicas"
  let selector x = Yzl.Builder.map x "selector"
  let matchLabels x = Yzl.Builder.map x "matchLabels"
  let template x = Yzl.Builder.map x "template"
  let containers x = Yzl.Builder.seq x "containers"
  let image (x:string) = Yzl.Builder.str x "image"
  let ports x = Yzl.Builder.seq x "ports"
  let containerPort x = Yzl.Builder.int x "containerPort"

  let objref x = Yzl.Builder.map x "objref"
  let fieldref x = Yzl.Builder.map x "fieldref"
  let fieldpath (value:string) = Yzl.Builder.str value "fieldpath"

  [<Tests>]
  let tests =
    testList "generate" [
      testCase "K8s deployment resource" <| fun _ ->
        
        let expected = File.ReadAllText("./yaml/k8s-deployment-resource.yaml")

        let yaml = ! [
          apiVersion "apps/v1"
          kind "Deployment"
          metadata [
            name "nginx-deployment"
            labels [
              app "nginx"
            ]
          ]
          spec [
            replicas 3
            selector [
              matchLabels [
                app "nginx"
              ]
            ]
            template [
              metadata [
                labels [
                  app "nginx"
                ]
              ]
              spec [
                containers [
                  ! [
                    name "nginx"
                    image "nginx:1.7.9"
                    ports [
                      ! [containerPort 80]
                    ]
                  ]
                  ! [
                    name "busybox"
                    image "busybox:1.0.0"
                    ports [
                      ! [containerPort 701]
                      ! [containerPort 901]
                    ]
                  ]
                ]
              ]
            ]         
          ]
        ]
  
        "Rendering failed" |> Expect.equal (Yzl.render yaml) expected

      test "Top-level sequence" {
        let expected = File.ReadAllText("./yaml/top-level-sequence.yaml")
          
        let single x = ! [
          name x
          objref [
            kind "ConfigMap"
            name "env-config"
            apiVersion "v1"
          ]
          fieldref [
            fieldpath ("metadata.annotations." + x.ToLower())
          ]
        ]

        let yaml = ! [
          single "A"
          single "B"
          single "C"
          single "D"
          single "E"
        ] 

        "Rendering failed" |> Expect.equal (Yzl.render yaml) expected
      }

      test "Sequence of maps" {

        let states x = Yzl.Builder.seq x "states"
        let state x = Yzl.Builder.map x "state"
        let code (x:string) = Yzl.Builder.str x "code"
        let expected = File.ReadAllText("./yaml/sequence-of-maps.yaml")
  
        let yaml2 = ! [
          ! [
            states [
            ! [ state [ code "OH" ]]
            ! [ state [ code "NO" ]]
           ]]]
        "Rendering failed" |> Expect.equal (Yzl.render yaml2) expected
      }

      test "Sequence of sequences" {

        let states x = Yzl.Builder.seq x "states"
        let state x = Yzl.Builder.map x "state"
        let code x = Yzl.Builder.str (x:string) "code"

        let expected = File.ReadAllText("./yaml/sequence-of-sequences.yaml")
        let yaml2 = ! [
          ! [
            states [
              ! [
                states [
                 ! [ state [ code "OH" ]]
                 ! [ state [ code "NO" ]]
                ]
              ]
              ! [
                states [
                 ! [ state [ code "MO" ]]
                 ! [ state [ code "MI" ]]
                ]
              ]
           
           ]]]
        "Rendering failed" |> Expect.equal (Yzl.render yaml2) expected
      }

      test "Bare sequence with one scalar" { 
          
          "Rendering failed" |> Expect.equal (! [! 5] |> Yzl.render) "- 5\n"
      }

      test "Bare sequence of sequence" { 
          
          "Rendering failed" |> Expect.equal (! [! [! true; ! true; ! true ]] |> Yzl.render)  "- \n  - true\n  - true\n  - true\n"
      }

      test "Should render true in lowercase" {
          "Rendering failed" |> Expect.equal (Yzl.Scalar(Yzl.Bool true) |> Yzl.render)  "true\n"
      }

      test "Should render false in lowercase" {
          "Rendering failed" |> Expect.equal (Yzl.Scalar(Yzl.Bool false) |> Yzl.render)  "false\n"
      }

      test "Should not render NoNode in seq" {
          "Rendering failed" |> Expect.equal (![ ! "x"; Yzl.NoNode; ! "y" ] |> Yzl.render)  "- x\n- y\n"
      }
      test "Should not render NoNode in map" {
          let actual = 
            ![ 
                Yzl.Named(Yzl.Name "name", ! "Value")
                Yzl.Builder.none
                Yzl.Named(Yzl.Name "name2", ! "Value2")
             ] |> Yzl.render
          "Rendering failed" |> Expect.equal actual  "name: Value\nname2: Value2\n"
      }
      
      test "Should render hybrid list" {
        "Rendering failed" |> Expect.equal (! [ ! "2"; ! true; ! 4 ] |> Yzl.render)  "- 2\n- true\n- 4\n"
      }

      test "Should render plain F# lists as sequences" {
        "Rendering failed" |> Expect.equal (! [ 2; 3; 4 ] |> Yzl.render)  "- 2\n- 3\n- 4\n"
        "Rendering failed" |> Expect.equal (! [ true; false; true ] |> Yzl.render)  "- true\n- false\n- true\n"
        "Rendering failed" |> Expect.equal (! [ "1"; "3"; "4" ] |> Yzl.render)  "- 1\n- 3\n- 4\n"
      }

      test "Should render empty sequence" {
        "Rendering failed" |> Expect.equal (([]: string list) |> Yzl.render)  "[]"
      }

      //TODO: fix this
      // test "Should render a list of NamedNode lists as a sequence of maps" {
      //   let expected = File.ReadAllText("./yaml/sequence-of-maps-2.yaml")

      //   let actual =
      //     ! [
      //       [
      //         name "n7"
      //         value "val7"
      //       ]
      //       [
      //         name "n8"
      //         value "val8"
      //       ]
      //     ]
      //   "Rendering failed" |> Expect.equal (actual |> Yzl.render) expected
      // }
    ]
