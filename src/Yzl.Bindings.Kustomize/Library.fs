namespace rec Yzl.Bindings.Kustomize
open Yzl.Core


/// Definition: ConfigMapArgs
module ConfigMapArgs = 
  let kVSources (value: Yzl.Node list) = Yzl.seq "kVSources" value
  let behavior (value: string) = Yzl.str "behavior" value
  let env (value: string) = Yzl.str "env" value
  let envs (value: Yzl.Node list) = Yzl.seq "envs" value
  let files (value: Yzl.Node list) = Yzl.seq "files" value
  let literals (value: Yzl.Node list) = Yzl.seq "literals" value
  let name (value: string) = Yzl.str "name" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
/// Definition: FieldSelector
module FieldSelector = 
  let fieldpath (value: string) = Yzl.str "fieldpath" value
/// Definition: GeneratorOptions
module GeneratorOptions = 
  let annotations (value: Yzl.NamedNode list) = Yzl.map "annotations" value
  let disableNameSuffixHash (value: bool) = Yzl.boolean "disableNameSuffixHash" value
  let labels (value: Yzl.NamedNode list) = Yzl.map "labels" value
/// Definition: Image
module Image = 
  let digest (value: string) = Yzl.str "digest" value
  let name (value: string) = Yzl.str "name" value
  let newName (value: string) = Yzl.str "newName" value
  let newTag (value: string) = Yzl.str "newTag" value
/// Definition: Inventory
module Inventory = 
  let configMap (value: Yzl.NamedNode list) = Yzl.map "configMap" value
  let ``type`` (value: string) = Yzl.str "type" value
/// Definition: KVSource
module KVSource = 
  let args (value: Yzl.Node list) = Yzl.seq "args" value
  let name (value: string) = Yzl.str "name" value
  let pluginType (value: string) = Yzl.str "pluginType" value
/// Definition: Kustomization
module Kustomization = 
  let apiVersion (value: string) = Yzl.str "apiVersion" value
  let bases (value: Yzl.Node list) = Yzl.seq "bases" value
  let commonAnnotations (value: Yzl.NamedNode list) = Yzl.map "commonAnnotations" value
  let commonLabels (value: Yzl.NamedNode list) = Yzl.map "commonLabels" value
  let configMapGenerator (value: Yzl.Node list) = Yzl.seq "configMapGenerator" value
  let configurations (value: Yzl.Node list) = Yzl.seq "configurations" value
  let crds (value: Yzl.Node list) = Yzl.seq "crds" value
  let generatorOptions (value: Yzl.NamedNode list) = Yzl.map "generatorOptions" value
  let generators (value: Yzl.Node list) = Yzl.seq "generators" value
  let images (value: Yzl.Node list) = Yzl.seq "images" value
  let inventory (value: Yzl.NamedNode list) = Yzl.map "inventory" value
  let kind (value: string) = Yzl.str "kind" value
  let namePrefix (value: string) = Yzl.str "namePrefix" value
  let nameSuffix (value: string) = Yzl.str "nameSuffix" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
  let patches (value: Yzl.Node list) = Yzl.seq "patches" value
  let patchesJson6902 (value: Yzl.Node list) = Yzl.seq "patchesJson6902" value
  let patchesStrategicMerge (value: Yzl.Node list) = Yzl.seq "patchesStrategicMerge" value
  let resources (value: Yzl.Node list) = Yzl.seq "resources" value
  let components (value: Yzl.Node list) = Yzl.seq "components" value
  let secretGenerator (value: Yzl.Node list) = Yzl.seq "secretGenerator" value
  let transformers (value: Yzl.Node list) = Yzl.seq "transformers" value
  let vars (value: Yzl.Node list) = Yzl.seq "vars" value
/// Definition: NameArgs
module NameArgs = 
  let name (value: string) = Yzl.str "name" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
/// Definition: PatchJson6902
module PatchJson6902 = 
  let path (value: string) = Yzl.str "path" value
  let target (value: Yzl.NamedNode list) = Yzl.map "target" value
/// Definition: PatchTarget
module PatchTarget = 
  let group (value: string) = Yzl.str "group" value
  let kind (value: string) = Yzl.str "kind" value
  let name (value: string) = Yzl.str "name" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
  let version (value: string) = Yzl.str "version" value
/// Definition: PatchTargetOptional
module PatchTargetOptional = 
  let group (value: string) = Yzl.str "group" value
  let kind (value: string) = Yzl.str "kind" value
  let name (value: string) = Yzl.str "name" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
  let version (value: string) = Yzl.str "version" value
  let labelSelector (value: string) = Yzl.str "labelSelector" value
  let annotationSelector (value: string) = Yzl.str "annotationSelector" value
/// Definition: PatchesPatchPath
module PatchesPatchPath = 
  let path (value: string) = Yzl.str "path" value
  let target (value: Yzl.NamedNode list) = Yzl.map "target" value
/// Definition: PatchesInlinePatch
module PatchesInlinePatch = 
  let patch (value: string) = Yzl.str "patch" value
  let target (value: Yzl.NamedNode list) = Yzl.map "target" value
/// Definition: SecretArgs
module SecretArgs = 
  let kVSources (value: Yzl.Node list) = Yzl.seq "kVSources" value
  let behavior (value: string) = Yzl.str "behavior" value
  let env (value: string) = Yzl.str "env" value
  let envs (value: Yzl.Node list) = Yzl.seq "envs" value
  let files (value: Yzl.Node list) = Yzl.seq "files" value
  let literals (value: Yzl.Node list) = Yzl.seq "literals" value
  let name (value: string) = Yzl.str "name" value
  let ``namespace`` (value: string) = Yzl.str "namespace" value
  let ``type`` (value: string) = Yzl.str "type" value
/// Definition: Target
module Target = 
  let apiVersion (value: string) = Yzl.str "apiVersion" value
  let group (value: string) = Yzl.str "group" value
  let kind (value: string) = Yzl.str "kind" value
  let name (value: string) = Yzl.str "name" value
  let version (value: string) = Yzl.str "version" value
/// Definition: Var
module Var = 
  let fieldref (value: Yzl.NamedNode list) = Yzl.map "fieldref" value
  let name (value: string) = Yzl.str "name" value
  let objref (value: Yzl.NamedNode list) = Yzl.map "objref" value
type Behavior1 = | Create | Replace | Merge 
type Behavior2 = | Create | Replace | Merge 
