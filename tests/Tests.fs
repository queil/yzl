namespace Yzl.Tests.Unit

module Core =

  open Expecto
  open Yzl.Core
  open System.IO

  let kind = Yzl.str "kind"
  let apiVersion = Yzl.str "apiVersion"
  let metadata = Yzl.map "metadata"
  let name = Yzl.str "name"
  let value = Yzl.str "value"
  let labels = Yzl.map "labels"
  let app = Yzl.str "app"
  let spec = Yzl.map "spec"
  let replicas = Yzl.int "replicas"
  let selector = Yzl.map "selector"
  let matchLabels = Yzl.map "matchLabels"
  let template = Yzl.map "template"
  let containers = Yzl.seq "containers"
  let image = Yzl.str "image"
  let ports = Yzl.seq "ports"
  let containerPort = Yzl.int "containerPort"

  let objref = Yzl.map "objref"
  let fieldref = Yzl.map "fieldref"
  let fieldpath = Yzl.str "fieldpath"

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

        let states = Yzl.seq "states"
        let state = Yzl.map "state"
        let code = Yzl.str "code"
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

        let states = Yzl.seq "states"
        let state = Yzl.map "state"
        let code = Yzl.str "code"

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
                Yzl.none
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
        "Rendering failed" |> Expect.equal (([]: string list) |> Yzl.render)  "[]\n"
      }

      test "Should render empty named sequence in-line" {
        let testSeq = Yzl.seq "testSeq"
        let testSeq2 =  Yzl.liftMany >> Yzl.seq "testSeq2"
        "Rendering failed" |> Expect.equal ([
          testSeq []
          testSeq2 [1;2;3]
          ] |> Yzl.render)  "testSeq: []\ntestSeq2:\n- 1\n- 2\n- 3\n"
      }

      test "Should render a list of NamedNode lists as a sequence of maps" {
        let expected = File.ReadAllText("./yaml/sequence-of-maps-2.yaml")

        let actual =
          ! [
            [
              name "n7"
              value "val7"
            ]
            [
              name "n8"
              value "val8"
            ]
          ]
        "Rendering failed" |> Expect.equal (actual |> Yzl.render) expected
      }

      test "Should render ad-hoc map" {
        let expected = """my: 44
very: 55
simple: |-
  66
map: true
seq:
- a
- b
- c
"""
        let actual = [
          "my" .= 44
          "very" .= "55"
          "simple" .= !|- "66"
          "map" .= true
          "seq" .= [
            "a"
            "b"
            "c"
          ]
        ]
        
        "Rendering failed" |> Expect.equal (actual |> Yzl.render) expected
      }

      test "Should render empty map as {}" {
        let empty = Yzl.map "empty"
        let empty2 = Yzl.map "empty2"

        let actual = [
          empty []
          "non-empty" .= [
            "some-other" .= "value"
          ]
        ]
        "Rendering failed" |> Expect.equal (actual |> Yzl.render) "empty: {}\nnon-empty:\n  some-other: value\n"
      }

    ]
