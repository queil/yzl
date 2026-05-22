namespace Yzl.Tests.Unit

module PatchTests =

  open Expecto
  open Yzl
  open Yzl.Patch
  open System.IO

  let items = Yzl.seq
  let item = Yzl.str

  let private prepareFile (fileName: string) (content: string) =
    let dir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
    Directory.CreateDirectory dir |> ignore
    let path = Path.Combine(dir, fileName)
    File.WriteAllText(path, content)
    path

  [<Tests>]
  let mergeTests =
    testList "patch merge" [

      test "Merge simple scalar into map" {
        let path =
          [ "name" .= "original"; "value" .= 123 ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlace [ ![ "value" .= 456; "extra" .= "new" ] ] path

        let expected =
          "name: original
value: 456
extra: new
"

        Expect.equal (File.ReadAllText path) expected "Should merge scalars into existing map"
      }

      test "Merge nested map" {
        let path =
          [ "config" .= [ "timeout" .= 30; "retries" .= 3 ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlace [ ![ "config" .= [ "retries" .= 5; "maxConnections" .= 100 ] ] ] path

        let expected =
          "config:
  timeout: 30
  retries: 5
  maxConnections: 100
"

        Expect.equal (File.ReadAllText path) expected "Should merge nested maps preserving existing keys"
      }

      test "Merge into sequence" {
        let path =
          items [ [ "name" .= "first" ]; [ "name" .= "second" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlace [ !(items [ [ "name" .= "third" ] ]) ] path

        let expected =
          "items:
- name: first
- name: second
- name: third
"

        Expect.equal (File.ReadAllText path) expected "Should merge sequence items"
      }

      test "MergeYzl should use Yzl magic indentation" {
        let path =
          items
            [ [ item
                  !|-"
                   - one: A
                     other: []
                   " ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlace
          [ ![ items
                 [ [ item
                       !|-"
                    - test: value
                      some: []
                    " ] ] ] ]
          path

        let expected =
          """items:
- item: |-
    - one: A
      other: []
- item: |-
    - test: value
      some: []
"""

        Expect.equal (File.ReadAllText path) expected "Should have correct content"
      }

      test "MergeYzlAt - simple property path" {
        let path =
          [ "database" .= [ "host" .= "localhost"; "port" .= 5432 ]
            "cache" .= [ "ttl" .= 3600 ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "host" .= "prod.db.com"; "maxConnections" .= 50 ] "database" path

        let expected =
          "database:
  host: prod.db.com
  port: 5432
  maxConnections: 50
cache:
  ttl: 3600
"

        Expect.equal (File.ReadAllText path) expected "Should merge at nested path preserving siblings"
      }

      test "MergeYzlAt - predicate-based path" {
        let path =
          items [ [ "name" .= "prod"; "port" .= 8080 ]; [ "name" .= "dev"; "port" .= 3000 ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "port" .= 8443; "ssl" .= true ] "items.[name=prod]" path

        let expected =
          "items:
- name: prod
  port: 8443
  ssl: true
- name: dev
  port: 3000
"

        Expect.equal (File.ReadAllText path) expected "Should merge into predicate-matched item"
      }

      test "MergeYzlAt - simple nested property chain" {
        let path =
          [ "servers"
            .= [ [ "name" .= "primary"; "config" .= [ "timeout" .= 30; "retries" .= 3 ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "timeout" .= 60; "maxQueue" .= 1000 ] "servers.0.config" path

        let expected =
          "servers:
- name: primary
  config:
    timeout: 60
    retries: 3
    maxQueue: 1000
"

        Expect.equal (File.ReadAllText path) expected "Should merge at deeply nested path with index"
      }

      test "MergeYzlAt - array index path" {
        let path =
          items [ [ "id" .= 1; "status" .= "active" ]; [ "id" .= 2; "status" .= "inactive" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "status" .= "archived"; "archived_at" .= "2024-01-01" ] "items.1" path

        let expected =
          "items:
- id: 1
  status: active
- id: 2
  status: archived
  archived_at: 2024-01-01
"

        Expect.equal (File.ReadAllText path) expected "Should merge at array index path"
      }

      test "MergeYzlAt with empty path targets root" {
        let path = [ "a" .= 1 ] |> Yzl.render |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "b" .= 2 ] "" path

        let expected =
          "a: 1
b: 2
"

        Expect.equal (File.ReadAllText path) expected "Should merge at root when path is empty"
      }

      test "MergeYzlAt - map in sequence by predicate" {
        let path =
          [ "servers"
            .= [ [ "hostname" .= "prod-1"; "port" .= 8080; "status" .= "active" ]
                 [ "hostname" .= "prod-2"; "port" .= 8081; "status" .= "inactive" ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "status" .= "archived"; "lastSeen" .= "2024-01-01" ] "servers.[hostname=prod-2]" path

        let expected =
          "servers:
- hostname: prod-1
  port: 8080
  status: active
- hostname: prod-2
  port: 8081
  status: archived
  lastSeen: 2024-01-01
"

        Expect.equal (File.ReadAllText path) expected "Should merge into sequence item by predicate"
      }

      test "MergeYzlAt - nested predicate key path" {
        let path =
          [ "patches"
            .= [ [ "patch" .= "original-patch"
                   "target" .= [ "kind" .= "ServiceAccount"; "name" .= "sa" ] ]
                 [ "patch" .= "other-patch"
                   "target" .= [ "kind" .= "ServiceAccount"; "name" .= "other" ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath ![ "patch" .= "updated-patch" ] "patches.[target.name=sa]" path

        let expected =
          "patches:
- patch: updated-patch
  target:
    kind: ServiceAccount
    name: sa
- patch: other-patch
  target:
    kind: ServiceAccount
    name: other
"

        Expect.equal (File.ReadAllText path) expected "Should merge into patches item matched by nested predicate target.name"
      }

      test "MergeYzlAt - replace scalar at nested predicate path" {
        let path =
          [ "patches"
            .= [ [ "patch"
                   .= !|-"""
                           - op: add
                             path: /zzz/yy
                             value: x
                         """
                   "target" .= [ "kind" .= "ServiceAccount"; "name" .= "sa" ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlaceAtPath
          (! """
            - op: add
              path: /zzz/yy
              value: lobsters
          """)
          "patches.[target.name=sa].patch"
          path

        let expected =
          "patches:
- patch: |-
    - op: add
      path: /zzz/yy
      value: lobsters
  target:
    kind: ServiceAccount
    name: sa
"

        Expect.equal (File.ReadAllText path) expected "Should replace scalar at patches.[target.name=sa].patch"
      }

      test "Merge into empty sequence uses block style" {
        let path =
          items []
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.editInPlace [ !(items [ [ "name" .= "first" ] ]) ] path

        let expected =
          "items:
- name: first
"

        Expect.equal (File.ReadAllText path) expected "Should merge into empty sequence with block style not flow style"
      }

    ]

  [<Tests>]
  let removeTests =
    testList "patch remove" [

      test "Remove node - simple key" {
        let path =
          items [ [ "name" .= "test"; "value" .= "123" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items.0.value" ]

        let expected =
          "items:
- name: test
"

        Expect.equal (File.ReadAllText path) expected "Should remove value field"
      }

      test "Remove node - by index" {
        let path =
          items [ [ "name" .= "first" ]; [ "name" .= "second" ]; [ "name" .= "third" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items.1" ]

        let expected =
          "items:
- name: first
- name: third
"

        Expect.equal (File.ReadAllText path) expected "Should remove second item"
      }

      test "Remove node - predicate with multiple matches" {
        let path =
          items [ [ "name" .= "prod"; "port" .= 8080 ]; [ "name" .= "dev"; "port" .= 3000 ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items.[name=dev]" ]

        let expected =
          "items:
- name: prod
  port: 8080
"

        Expect.equal (File.ReadAllText path) expected "Should remove dev item"
      }

      test "Remove node - nested path" {
        let path =
          [ "database" .= [ "host" .= "localhost"; "port" .= 5432; "password" .= "secret" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "database.password" ]

        let expected =
          "database:
  host: localhost
  port: 5432
"

        Expect.equal (File.ReadAllText path) expected "Should remove password"
      }

      test "Remove node - predicate mid-path" {
        let path =
          [ "servers"
            .= [ [ "name" .= "prod"; "config" .= [ "timeout" .= 30; "retries" .= 3 ] ]
                 [ "name" .= "dev"; "config" .= [ "timeout" .= 10; "retries" .= 1 ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "servers.[name=prod].config.retries" ]

        let expected =
          "servers:
- name: prod
  config:
    timeout: 30
- name: dev
  config:
    timeout: 10
    retries: 1
"

        Expect.equal (File.ReadAllText path) expected "Should remove retries from prod only"
      }

      test "Remove node - top level key" {
        let path =
          [ "version" .= "1.0"; "name" .= "myapp"; "debug" .= true ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ ".debug" ]

        let expected =
          "version: '1.0'
name: myapp
"

        Expect.equal (File.ReadAllText path) expected "Should remove debug key"
      }

      test "Remove node - entire array" {
        let path =
          [ "items" .= [ [ "id" .= 1 ]; [ "id" .= 2 ] ]; "other" .= "data" ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items" ]

        let expected =
          "other: data
"

        Expect.equal (File.ReadAllText path) expected "Should remove entire items array"
      }

      test "Remove node - scalar sequence by value" {
        let path =
          [ "tags" .= [ "production"; "debug"; "experimental" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "tags.[debug]" ]

        let expected =
          "tags:
- production
- experimental
"

        Expect.equal (File.ReadAllText path) expected "Should remove debug tag"
      }

      test "Remove node - predicate with quoted value" {
        let path =
          items
            [ [ "url" .= "https://api.example.com"; "name" .= "api" ]
              [ "url" .= "https://web.example.com"; "name" .= "web" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items.[url=\"https://api.example.com\"]" ]

        let expected =
          "items:
- url: https://web.example.com
  name: web
"

        Expect.equal (File.ReadAllText path) expected "Should remove api item"
      }

      test "Remove node - predicate with special chars in key" {
        let path =
          items
            [ [ "app.kubernetes.io/name" .= "myapp"; "version" .= "1.0" ]
              [ "app.kubernetes.io/name" .= "other"; "version" .= "2.0" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "items.[app.kubernetes.io/name=myapp]" ]

        let expected =
          "items:
- app.kubernetes.io/name: other
  version: '2.0'
"

        Expect.equal (File.ReadAllText path) expected "Should remove myapp item"
      }

      test "Remove node - deep nested with multiple predicates" {
        let path =
          [ "environments"
            .= [ [ "name" .= "prod"
                   "servers"
                   .= [ [ "hostname" .= "prod-1"; "port" .= 8080 ]
                        [ "hostname" .= "prod-2"; "port" .= 8081 ] ] ]
                 [ "name" .= "dev"; "servers" .= [ [ "hostname" .= "dev-1"; "port" .= 3000 ] ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "environments.[name=prod].servers.[hostname=prod-2]" ]

        let expected =
          "environments:
- name: prod
  servers:
  - hostname: prod-1
    port: 8080
- name: dev
  servers:
  - hostname: dev-1
    port: 3000
"

        Expect.equal (File.ReadAllText path) expected "Should remove prod-2 server only"
      }

      test "Remove node - scalar with spaces" {
        let path =
          [ "commands" .= [ "npm start"; "npm test"; "npm run build" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "commands.[\"npm test\"]" ]

        let expected =
          "commands:
- npm start
- npm run build
"

        Expect.equal (File.ReadAllText path) expected "Should remove npm test command"
      }

      test "Remove node - array index at root level" {
        let path =
          [ [ "name" .= "first" ]; [ "name" .= "second" ]; [ "name" .= "third" ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "0" ]

        let expected =
          "- name: second
- name: third
"

        Expect.equal (File.ReadAllText path) expected "Should remove first item from root array"
      }

      test "Remove node - nested predicate then key" {
        let path =
          [ "clusters"
            .= [ [ "name" .= "us-east"; "config" .= [ "replicas" .= 3; "autoscale" .= true ] ]
                 [ "name" .= "eu-west"; "config" .= [ "replicas" .= 5; "autoscale" .= false ] ] ] ]
          |> Yzl.render
          |> prepareFile "test.yaml"

        Patch.removeNodes path [ "clusters.[name=eu-west].config.autoscale" ]

        let expected =
          "clusters:
- name: us-east
  config:
    replicas: 3
    autoscale: true
- name: eu-west
  config:
    replicas: 5
"

        Expect.equal (File.ReadAllText path) expected "Should remove autoscale from eu-west only"
      }

    ]
