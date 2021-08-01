namespace Yzl.Tests.Unit

module Bindings =

  open Expecto
  open Yzl.Core
  open Yzl.Bindings.Kustomize

  [<Tests>]
  let tests =
    testList "Bindings" [
      
      test "Kustomize" {

        let expected = 
          """kind: Kustomization
apiVersion: kustomize.config.k8s.io
images:
- name: busybox
  newName: queil/busybox
- name: nginx
  newTag: queil/nginx
patchesJson6902:
- path: ./paches/ingress.yaml
  target:
    group: networking.k8s.io
    version: v1
    kind: Ingress
    name: ingress-app-1
    namespace: app-1
configMapGenerator:
- behavior: merge
  envs:
  - one.env
  - two.env
  name: cm
"""

        let yaml = Kustomization.New <| fun y -> [
          y.kind "Kustomization"
          y.apiVersion "kustomize.config.k8s.io"
          y.images <| fun y -> [
          [ y.name "busybox"
            y.newName "queil/busybox"
          ]
          [ y.name "nginx"
            y.newTag "queil/nginx"
          ]]
          y.patchesJson6902 <| fun y -> [
          [
            y.path "./paches/ingress.yaml"
            y.target <| fun y -> [
              y.group "networking.k8s.io"
              y.version "v1"
              y.kind "Ingress"
              y.name "ingress-app-1"
              y.``namespace`` "app-1"
            ]
          ]]
          y.configMapGenerator <| fun y -> [
            [
              y.behavior "merge"
              y.envs ["one.env"; "two.env"]
              y.name "cm"
            ]
          ]
        ]

        "Rendering failed" |> Expect.equal (! yaml |> Yzl.render) expected  
      }
    ]
