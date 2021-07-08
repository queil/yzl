#r "nuget: Yzl"
namespace rec Yzl.Bindings.Kustomize
open Yzl.Core


/// Definition: ConfigMapArgs
type ConfigMapArgs = {
  KVSources: KVSource list option
  Behavior: Behavior1 option
  Env: string option
  Envs: string list option
  Files: string list option
  Literals: string list option
  Name: string option
  Namespace: string option
}
with
  static
    member Default =
    {
      KVSources = None
      Behavior = None
      Env = None
      Envs = None
      Files = None
      Literals = None
      Name = None
      Namespace = None
}

/// Definition: FieldSelector
type FieldSelector = {
  Fieldpath: string option
}
with
  static
    member Default =
    {
      Fieldpath = None
}

/// Definition: GeneratorOptions
type GeneratorOptions = {
  Annotations: Yzl.NamedNode list option
  DisableNameSuffixHash: bool option
  Labels: Yzl.NamedNode list option
}
with
  static
    member Default =
    {
      Annotations = None
      DisableNameSuffixHash = None
      Labels = None
}

/// Definition: Image
type Image = {
  Digest: string option
  Name: string option
  NewName: string option
  NewTag: string option
}
with
  static
    member Default =
    {
      Digest = None
      Name = None
      NewName = None
      NewTag = None
}

/// Definition: Inventory
type Inventory = {
  ConfigMap: NameArgs option
  Type: string option
}
with
  static
    member Default =
    {
      ConfigMap = None
      Type = None
}

/// Definition: KVSource
type KVSource = {
  Args: string list option
  Name: string option
  PluginType: string option
}
with
  static
    member Default =
    {
      Args = None
      Name = None
      PluginType = None
}

/// Definition: Kustomization
type Kustomization = {
  ApiVersion: string option
  Bases: string list option
  CommonAnnotations: Yzl.NamedNode list option
  CommonLabels: Yzl.NamedNode list option
  ConfigMapGenerator: ConfigMapArgs list option
  Configurations: string list option
  Crds: string list option
  GeneratorOptions: GeneratorOptions option
  Generators: string list option
  Images: Image list option
  Inventory: Inventory option
  Kind: string option
  NamePrefix: string option
  NameSuffix: string option
  Namespace: string option
  Patches: Union2 list option
  PatchesJson6902: PatchJson6902 list option
  PatchesStrategicMerge: string list option
  Resources: string list option
  Components: string list option
  SecretGenerator: SecretArgs list option
  Transformers: string list option
  Vars: Var list option
}
with
  static
    member Default =
    {
      ApiVersion = None
      Bases = None
      CommonAnnotations = None
      CommonLabels = None
      ConfigMapGenerator = None
      Configurations = None
      Crds = None
      GeneratorOptions = None
      Generators = None
      Images = None
      Inventory = None
      Kind = None
      NamePrefix = None
      NameSuffix = None
      Namespace = None
      Patches = None
      PatchesJson6902 = None
      PatchesStrategicMerge = None
      Resources = None
      Components = None
      SecretGenerator = None
      Transformers = None
      Vars = None
  }
  static member yzl(?images: Image list, ?patches: Union2 list) =
    {
      Kustomization.Default with
        Images = images
        Patches = patches
    }


module x =
  let kind (value: string) (x: Kustomization) = { x with Kind = Some value }
  
  let ee =
    Kustomization.yzl(
      images = [
      ],
      patches = [
        Union2.PatchesInlinePatch
      ]
    )

  let kustomization = { 
    Kustomization.Default with
      Kind = Some "kustom"
      ApiVersion = Some "x"
      Resources = Some ["y"; "b"]
  }

  let kind (value: string) (x: Kustomization) = { x with Kind = Some value }
  let apiVersion (value: string) (x: Kustomization) = { x with Kind = Some value }


  let kustomization2 = Kustomization.Default

  let result =
    Kustomization.Default
    |> kind "x"
    |> apiVersion "test"


  let result2: (Kustomization -> Kustomization) list = [
    kind "xxx"
    apiVersion "cccc"
  ]

  let kustomization23 (xs: (Kustomization -> Kustomization) list) = xs |> List.fold (fun state t -> t state) Kustomization.Default 
  
  let b =
    kustomization23 [
      kind "xxx"
      apiVersion "ccc"
      
    ]

/// Definition: NameArgs
type NameArgs = {
  Name: string option
  Namespace: string option
}
with
  static
    member Default =
    {
      Name = None
      Namespace = None
}

/// Definition: PatchJson6902
type PatchJson6902 = {
  Path: string option
  Target: PatchTarget option
}
with
  static
    member Default =
    {
      Path = None
      Target = None
}

/// Definition: PatchTarget
type PatchTarget = {
  Group: string option
  Kind: string option
  Name: string option
  Namespace: string option
  Version: string option
}
with
  static
    member Default =
    {
      Group = None
      Kind = None
      Name = None
      Namespace = None
      Version = None
}

/// Definition: PatchTargetOptional
type PatchTargetOptional = {
  Group: string option
  Kind: string option
  Name: string option
  Namespace: string option
  Version: string option
  LabelSelector: string option
  AnnotationSelector: string option
}
with
  static
    member Default =
    {
      Group = None
      Kind = None
      Name = None
      Namespace = None
      Version = None
      LabelSelector = None
      AnnotationSelector = None
}

/// Definition: PatchesPatchPath
type PatchesPatchPath = {
  Path: string option
  Target: PatchTargetOptional option
}
with
  static
    member Default =
    {
      Path = None
      Target = None
}

/// Definition: PatchesInlinePatch
type PatchesInlinePatch = {
  Patch: string option
  Target: PatchTargetOptional option
}
with
  static
    member Default =
    {
      Patch = None
      Target = None
}

/// Definition: SecretArgs
type SecretArgs = {
  KVSources: KVSource list option
  Behavior: Behavior3 option
  Env: string option
  Envs: string list option
  Files: string list option
  Literals: string list option
  Name: string option
  Namespace: string option
  Type: string option
}
with
  static
    member Default =
    {
      KVSources = None
      Behavior = None
      Env = None
      Envs = None
      Files = None
      Literals = None
      Name = None
      Namespace = None
      Type = None
}

/// Definition: Target
type Target = {
  ApiVersion: string option
  Group: string option
  Kind: string option
  Name: string option
  Version: string option
}
with
  static
    member Default =
    {
      ApiVersion = None
      Group = None
      Kind = None
      Name = None
      Version = None
}

/// Definition: Var
type Var = {
  Fieldref: FieldSelector option
  Name: string option
  Objref: Target option
}
with
  static
    member Default =
    {
      Fieldref = None
      Name = None
      Objref = None
}
type Behavior1 = | Create | Replace | Merge 
type Union2 = | PatchesPatchPath | PatchesInlinePatch 
type Behavior3 = | Create | Replace | Merge 
