module Tests

open Expecto
open Yzl.Core
open Yzl.Operators

let kind = Yzl.str "kind"
let apiVersion = Yzl.str "apiVersion"
let metadata x = Yzl.map "metadata" x
let name = Yzl.str "name"
let labels x = Yzl.map "labels" x
let app = Yzl.str "app"
let spec x = Yzl.map "spec" x
let replicas = Yzl.int "replicas"
let selector x = Yzl.map "selector" x
let matchLabels x = Yzl.map "matchLabels" x
let template x = Yzl.map "template" x
let containers x = Yzl.seq "containers" x
let image = Yzl.str "image"
let ports x = Yzl.seq "ports" x
let containerPort = Yzl.int "containerPort"

let objref x = Yzl.map "objref" x
let fieldref x = Yzl.map "fieldref" x
let fieldpath = Yzl.str "fieldpath"

[<Tests>]
let tests =
  testList "generate" [
    testCase "universe exists (╭ರᴥ•́)" <| fun _ ->

      let yaml = !% [
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
                !- [
                  name "nginx"
                  image "nginx:1.7.9"
                  ports [
                    !- [containerPort 80]
                  ]
                ]
                !- [
                  name "busybox"
                  image "busybox:1.0.0"
                  ports [
                    !- [containerPort 701]
                    !- [containerPort 901]
                  ]
                ]
              ]
            ]
          ]         
        ]
      ]
      
      let expected = """apiVersion: apps/v1
kind: Deployment
metadata:
  name: nginx-deployment
  labels:
    app: nginx
spec:
  replicas: 3
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
      - name: nginx
        image: nginx:1.7.9
        ports:
        - containerPort: 80
      - name: busybox
        image: busybox:1.0.0
        ports:
        - containerPort: 701
        - containerPort: 901
"""
      Expect.equal (Yzl.renderYaml {indentSpaces = 2} yaml) expected "I compute, therefore I am."

    
    test "Top-level sequence" {
      let expected = """- name: A
  objref:
    kind: ConfigMap
    name: env-config
    apiVersion: v1
  fieldref:
    fieldpath: metadata.annotations.a
- name: B
  objref:
    kind: ConfigMap
    name: env-config
    apiVersion: v1
  fieldref:
    fieldpath: metadata.annotations.b
- name: C
  objref:
    kind: ConfigMap
    name: env-config
    apiVersion: v1
  fieldref:
    fieldpath: metadata.annotations.c
- name: D
  objref:
    kind: ConfigMap
    name: env-config
    apiVersion: v1
  fieldref:
    fieldpath: metadata.annotations.d
- name: E
  objref:
    kind: ConfigMap
    name: env-config
    apiVersion: v1
  fieldref:
    fieldpath: metadata.annotations.e
"""
      let single x = !% [
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

      let yaml = !-- [
        !- (single "A")
        !- (single "B")
        !- (single "C")
        !- (single "D")
        !- (single "E")
      ] 

      "Rendering failed" |> Expect.equal (Yzl.renderYaml {indentSpaces = 2} yaml) expected
    }

    test "Sequence of maps" {

      let states x = Yzl.seq "states" x
      let state x = Yzl.map "state" x
      let code x = Yzl.str "code" x
      let expected = """- states:
  - state:
      code: OH
  - state:
      code: NO
"""

      let yaml2 = !-- [
        !- [
          states [
          !- [ state [ code "OH" ]]
          !- [ state [ code "NO" ]]
         ]]]
      "Rendering failed" |> Expect.equal (Yzl.renderYaml {indentSpaces=2} yaml2)  expected
    }

    test "Sequence of sequences" {

      let states x = Yzl.seq "states" x
      let state x = Yzl.map "state" x
      let code x = Yzl.str "code" x
      let expected = """- states:
  - states:
    - state:
        code: OH
    - state:
        code: NO
  - states:
    - state:
        code: MO
    - state:
        code: MI
"""
      let yaml2 = !-- [
        !- [
          states [
            !- [
              states [
               !- [ state [ code "OH" ]]
               !- [ state [ code "NO" ]]
              ]
            ]
            !- [
              states [
               !- [ state [ code "MO" ]]
               !- [ state [ code "MI" ]]
              ]
            ]
         
         ]]]
      "Rendering failed" |> Expect.equal (Yzl.renderYaml {indentSpaces=2} yaml2)  expected
    }
  ]
