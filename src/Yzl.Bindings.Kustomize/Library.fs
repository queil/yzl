namespace rec Yzl.Bindings.Kustomize
open Yzl.Core


/// Definition: ConfigMapArgs
type ConfigMapArgs() = 
  member _.kVSources (value: KVSource -> Yzl.NamedNode list list) = Yzl.seq "kVSources" (value KVSource.Default |> Yzl.liftMany)
  member _.behavior (value: string) = Yzl.str "behavior" value
  member _.env (value: string) = Yzl.str "env" value
  member _.envs (value: string list) = Yzl.seq "envs" (value |> Yzl.liftMany)
  member _.files (value: string list) = Yzl.seq "files" (value |> Yzl.liftMany)
  member _.literals (value: string list) = Yzl.seq "literals" (value |> Yzl.liftMany)
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  static member Default = ConfigMapArgs()
  static member New (creator:ConfigMapArgs -> Yzl.NamedNode list) : Yzl.Node = creator ConfigMapArgs.Default |> Yzl.lift

/// Definition: FieldSelector
type FieldSelector() = 
  member _.fieldpath (value: string) = Yzl.str "fieldpath" value
  static member Default = FieldSelector()
  static member New (creator:FieldSelector -> Yzl.NamedNode list) : Yzl.Node = creator FieldSelector.Default |> Yzl.lift

/// Definition: GeneratorOptions
type GeneratorOptions() = 
  member _.annotations (value: Yzl.NamedNode list) = Yzl.map "annotations" value
  member _.disableNameSuffixHash (value: bool) = Yzl.boolean "disableNameSuffixHash" value
  member _.labels (value: Yzl.NamedNode list) = Yzl.map "labels" value
  static member Default = GeneratorOptions()
  static member New (creator:GeneratorOptions -> Yzl.NamedNode list) : Yzl.Node = creator GeneratorOptions.Default |> Yzl.lift

/// Definition: Image
type Image() = 
  member _.digest (value: string) = Yzl.str "digest" value
  member _.name (value: string) = Yzl.str "name" value
  member _.newName (value: string) = Yzl.str "newName" value
  member _.newTag (value: string) = Yzl.str "newTag" value
  static member Default = Image()
  static member New (creator:Image -> Yzl.NamedNode list) : Yzl.Node = creator Image.Default |> Yzl.lift

/// Definition: Inventory
type Inventory() = 
  member _.configMap (value: NameArgs -> Yzl.NamedNode list) = Yzl.map "configMap" (value NameArgs.Default)
  member _.``type`` (value: string) = Yzl.str "type" value
  static member Default = Inventory()
  static member New (creator:Inventory -> Yzl.NamedNode list) : Yzl.Node = creator Inventory.Default |> Yzl.lift

/// Definition: KVSource
type KVSource() = 
  member _.args (value: string list) = Yzl.seq "args" (value |> Yzl.liftMany)
  member _.name (value: string) = Yzl.str "name" value
  member _.pluginType (value: string) = Yzl.str "pluginType" value
  static member Default = KVSource()
  static member New (creator:KVSource -> Yzl.NamedNode list) : Yzl.Node = creator KVSource.Default |> Yzl.lift

/// Definition: Kustomization
type Kustomization() = 
  member _.apiVersion (value: string) = Yzl.str "apiVersion" value
  member _.bases (value: string list) = Yzl.seq "bases" (value |> Yzl.liftMany)
  member _.commonAnnotations (value: Yzl.NamedNode list) = Yzl.map "commonAnnotations" value
  member _.commonLabels (value: Yzl.NamedNode list) = Yzl.map "commonLabels" value
  member _.configMapGenerator (value: ConfigMapArgs -> Yzl.NamedNode list list) = Yzl.seq "configMapGenerator" (value ConfigMapArgs.Default |> Yzl.liftMany)
  member _.configurations (value: string list) = Yzl.seq "configurations" (value |> Yzl.liftMany)
  member _.crds (value: string list) = Yzl.seq "crds" (value |> Yzl.liftMany)
  member _.generatorOptions (value: GeneratorOptions -> Yzl.NamedNode list) = Yzl.map "generatorOptions" (value GeneratorOptions.Default)
  member _.generators (value: string list) = Yzl.seq "generators" (value |> Yzl.liftMany)
  member _.images (value: Image -> Yzl.NamedNode list list) = Yzl.seq "images" (value Image.Default |> Yzl.liftMany)
  member _.inventory (value: Inventory -> Yzl.NamedNode list) = Yzl.map "inventory" (value Inventory.Default)
  member _.kind (value: string) = Yzl.str "kind" value
  member _.namePrefix (value: string) = Yzl.str "namePrefix" value
  member _.nameSuffix (value: string) = Yzl.str "nameSuffix" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  member _.patches (value: Yzl.Node list) = Yzl.seq "patches" (value |> Yzl.liftMany)
  member _.patchesJson6902 (value: PatchJson6902 -> Yzl.NamedNode list list) = Yzl.seq "patchesJson6902" (value PatchJson6902.Default |> Yzl.liftMany)
  member _.patchesStrategicMerge (value: string list) = Yzl.seq "patchesStrategicMerge" (value |> Yzl.liftMany)
  member _.resources (value: string list) = Yzl.seq "resources" (value |> Yzl.liftMany)
  member _.components (value: string list) = Yzl.seq "components" (value |> Yzl.liftMany)
  member _.secretGenerator (value: SecretArgs -> Yzl.NamedNode list list) = Yzl.seq "secretGenerator" (value SecretArgs.Default |> Yzl.liftMany)
  member _.transformers (value: string list) = Yzl.seq "transformers" (value |> Yzl.liftMany)
  member _.vars (value: Var -> Yzl.NamedNode list list) = Yzl.seq "vars" (value Var.Default |> Yzl.liftMany)
  static member Default = Kustomization()
  static member New (creator:Kustomization -> Yzl.NamedNode list) : Yzl.Node = creator Kustomization.Default |> Yzl.lift

/// Definition: NameArgs
type NameArgs() = 
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  static member Default = NameArgs()
  static member New (creator:NameArgs -> Yzl.NamedNode list) : Yzl.Node = creator NameArgs.Default |> Yzl.lift

/// Definition: PatchJson6902
type PatchJson6902() = 
  member _.path (value: string) = Yzl.str "path" value
  member _.target (value: PatchTarget -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTarget.Default)
  static member Default = PatchJson6902()
  static member New (creator:PatchJson6902 -> Yzl.NamedNode list) : Yzl.Node = creator PatchJson6902.Default |> Yzl.lift

/// Definition: PatchTarget
type PatchTarget() = 
  member _.group (value: string) = Yzl.str "group" value
  member _.kind (value: string) = Yzl.str "kind" value
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  member _.version (value: string) = Yzl.str "version" value
  static member Default = PatchTarget()
  static member New (creator:PatchTarget -> Yzl.NamedNode list) : Yzl.Node = creator PatchTarget.Default |> Yzl.lift

/// Definition: PatchTargetOptional
type PatchTargetOptional() = 
  member _.group (value: string) = Yzl.str "group" value
  member _.kind (value: string) = Yzl.str "kind" value
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  member _.version (value: string) = Yzl.str "version" value
  member _.labelSelector (value: string) = Yzl.str "labelSelector" value
  member _.annotationSelector (value: string) = Yzl.str "annotationSelector" value
  static member Default = PatchTargetOptional()
  static member New (creator:PatchTargetOptional -> Yzl.NamedNode list) : Yzl.Node = creator PatchTargetOptional.Default |> Yzl.lift

/// Definition: PatchesPatchPath
type PatchesPatchPath() = 
  member _.path (value: string) = Yzl.str "path" value
  member _.target (value: PatchTargetOptional -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTargetOptional.Default)
  static member Default = PatchesPatchPath()
  static member New (creator:PatchesPatchPath -> Yzl.NamedNode list) : Yzl.Node = creator PatchesPatchPath.Default |> Yzl.lift

/// Definition: PatchesInlinePatch
type PatchesInlinePatch() = 
  member _.patch (value: string) = Yzl.str "patch" value
  member _.target (value: PatchTargetOptional -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTargetOptional.Default)
  static member Default = PatchesInlinePatch()
  static member New (creator:PatchesInlinePatch -> Yzl.NamedNode list) : Yzl.Node = creator PatchesInlinePatch.Default |> Yzl.lift

/// Definition: SecretArgs
type SecretArgs() = 
  member _.kVSources (value: KVSource -> Yzl.NamedNode list list) = Yzl.seq "kVSources" (value KVSource.Default |> Yzl.liftMany)
  member _.behavior (value: string) = Yzl.str "behavior" value
  member _.env (value: string) = Yzl.str "env" value
  member _.envs (value: string list) = Yzl.seq "envs" (value |> Yzl.liftMany)
  member _.files (value: string list) = Yzl.seq "files" (value |> Yzl.liftMany)
  member _.literals (value: string list) = Yzl.seq "literals" (value |> Yzl.liftMany)
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  member _.``type`` (value: string) = Yzl.str "type" value
  static member Default = SecretArgs()
  static member New (creator:SecretArgs -> Yzl.NamedNode list) : Yzl.Node = creator SecretArgs.Default |> Yzl.lift

/// Definition: Target
type Target() = 
  member _.apiVersion (value: string) = Yzl.str "apiVersion" value
  member _.group (value: string) = Yzl.str "group" value
  member _.kind (value: string) = Yzl.str "kind" value
  member _.name (value: string) = Yzl.str "name" value
  member _.version (value: string) = Yzl.str "version" value
  static member Default = Target()
  static member New (creator:Target -> Yzl.NamedNode list) : Yzl.Node = creator Target.Default |> Yzl.lift

/// Definition: Var
type Var() = 
  member _.fieldref (value: FieldSelector -> Yzl.NamedNode list) = Yzl.map "fieldref" (value FieldSelector.Default)
  member _.name (value: string) = Yzl.str "name" value
  member _.objref (value: Target -> Yzl.NamedNode list) = Yzl.map "objref" (value Target.Default)
  static member Default = Var()
  static member New (creator:Var -> Yzl.NamedNode list) : Yzl.Node = creator Var.Default |> Yzl.lift

type Behavior1 = | Create | Replace | Merge 
type Behavior2 = | Create | Replace | Merge 
