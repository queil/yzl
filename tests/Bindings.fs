namespace Yzl.Tests.Unit

module Bindings =

  open Expecto
  open Yzl.Core
  open Yzl.Bindings.Kustomize

  open type Yzl.Bindings.Kustomize.PatchJson6902
  open type Yzl.Bindings.Kustomize.PatchTarget
  open type Yzl.Bindings.Kustomize.Image
  open type Yzl.Bindings.Kustomize.ConfigMapArgs 
  open type Yzl.Bindings.Kustomize.Kustomization
  open type Yzl.Bindings.Kustomize.Common
  


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
- target:
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

        let yaml = Kustomization.yzl [
          kind "Kustomization"
          apiVersion "kustomize.config.k8s.io"
          images [
            [
              name "busybox"
              newName "queil/busybox"
            ]
            [
              name "nginx"
              newTag "queil/nginx"
            ]
          ]
          patchesJson6902 [
            [
              target [
                group "networking.k8s.io"
                version "v1"
                kind "Ingress"
                name "ingress-app-1"
                ``namespace`` "app-1"
              ]
            ]
          ]
          configMapGenerator [
            [
              behavior "merge"
              envs [
                "one.env"
                "two.env"
              ]
              name "cm"
            ]
          ]
        ]

        "Rendering failed" |> Expect.equal (yaml |> Yzl.render) expected  
      }
    ]
