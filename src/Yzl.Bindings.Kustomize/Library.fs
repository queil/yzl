namespace rec Yzl.Bindings.Kustomize
open Yzl.Core


/// ConfigMapArgs - ConfigMapArgs contains the metadata of how to generate a configmap
type ConfigMapArgs() = 
  member _.kVSources (value: KVSource -> Yzl.NamedNode list list) = Yzl.seq "kVSources" (value KVSource.Default |> Yzl.liftMany)
  member _.behavior (value: string) = Yzl.str "behavior" value
  /// Deprecated.  Use envs instead.
  member _.env (value: string) = Yzl.str "env" value
  /// A list of file paths. The contents of each file should be one key=value pair per line
  member _.envs (value: string list) = Yzl.seq "envs" (value |> Yzl.liftMany)
  /// A list of file sources to use in creating a list of key, value pairs
  member _.files (value: string list) = Yzl.seq "files" (value |> Yzl.liftMany)
  /// A list of literal pair sources. Each literal source should be a key and literal value, e.g. `key=value`
  member _.literals (value: string list) = Yzl.seq "literals" (value |> Yzl.liftMany)
  /// Name - actually the partial name - of the generated resource
  member _.name (value: string) = Yzl.str "name" value
  /// Namespace for the configmap, optional
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  static member Default = ConfigMapArgs()
  static member New (creator:ConfigMapArgs -> Yzl.NamedNode list) : Yzl.Node = creator ConfigMapArgs.Default |> Yzl.lift

/// FieldSelector - Contains the fieldPath to an object field
type FieldSelector() = 
  member _.fieldpath (value: string) = Yzl.str "fieldpath" value
  static member Default = FieldSelector()
  static member New (creator:FieldSelector -> Yzl.NamedNode list) : Yzl.Node = creator FieldSelector.Default |> Yzl.lift

/// GeneratorOptions - GeneratorOptions modify behavior of all ConfigMap and Secret generators
type GeneratorOptions() = 
  /// Annotations to add to all generated resources
  member _.annotations (value: Yzl.NamedNode list) = Yzl.map "annotations" value
  /// DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents
  member _.disableNameSuffixHash (value: bool) = Yzl.boolean "disableNameSuffixHash" value
  /// Labels to add to all generated resources
  member _.labels (value: Yzl.NamedNode list) = Yzl.map "labels" value
  static member Default = GeneratorOptions()
  static member New (creator:GeneratorOptions -> Yzl.NamedNode list) : Yzl.Node = creator GeneratorOptions.Default |> Yzl.lift

/// Image
type Image() = 
  member _.digest (value: string) = Yzl.str "digest" value
  member _.name (value: string) = Yzl.str "name" value
  member _.newName (value: string) = Yzl.str "newName" value
  member _.newTag (value: string) = Yzl.str "newTag" value
  static member Default = Image()
  static member New (creator:Image -> Yzl.NamedNode list) : Yzl.Node = creator Image.Default |> Yzl.lift

/// Inventory - Inventory appends an object that contains the record of all other objects, which can be used in apply, prune and delete
type Inventory() = 
  member _.configMap (value: NameArgs -> Yzl.NamedNode list) = Yzl.map "configMap" (value NameArgs.Default)
  member _.``type`` (value: string) = Yzl.str "type" value
  static member Default = Inventory()
  static member New (creator:Inventory -> Yzl.NamedNode list) : Yzl.Node = creator Inventory.Default |> Yzl.lift

/// KVSource
type KVSource() = 
  member _.args (value: string list) = Yzl.seq "args" (value |> Yzl.liftMany)
  member _.name (value: string) = Yzl.str "name" value
  member _.pluginType (value: string) = Yzl.str "pluginType" value
  static member Default = KVSource()
  static member New (creator:KVSource -> Yzl.NamedNode list) : Yzl.Node = creator KVSource.Default |> Yzl.lift

/// Kustomization
type Kustomization() = 
  member _.apiVersion (value: string) = Yzl.str "apiVersion" value
  /// Bases are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file.
  member _.bases (value: string list) = Yzl.seq "bases" (value |> Yzl.liftMany)
  /// CommonAnnotations to add to all objects
  member _.commonAnnotations (value: Yzl.NamedNode list) = Yzl.map "commonAnnotations" value
  ///  CommonLabels to add to all objects and selectors
  member _.commonLabels (value: Yzl.NamedNode list) = Yzl.map "commonLabels" value
  /// ConfigMapGenerator is a list of configmaps to generate from local data (one configMap per list item)
  member _.configMapGenerator (value: ConfigMapArgs -> Yzl.NamedNode list list) = Yzl.seq "configMapGenerator" (value ConfigMapArgs.Default |> Yzl.liftMany)
  /// Configurations is a list of transformer configuration files
  member _.configurations (value: string list) = Yzl.seq "configurations" (value |> Yzl.liftMany)
  /// Crds specifies relative paths to Custom Resource Definition files. This allows custom resources to be recognized as operands, making it possible to add them to the Resources list. CRDs themselves are not modified.
  member _.crds (value: string list) = Yzl.seq "crds" (value |> Yzl.liftMany)
  member _.generatorOptions (value: GeneratorOptions -> Yzl.NamedNode list) = Yzl.map "generatorOptions" (value GeneratorOptions.Default)
  /// Generators is a list of files containing custom generators
  member _.generators (value: string list) = Yzl.seq "generators" (value |> Yzl.liftMany)
  /// Images is a list of (image name, new name, new tag or digest) for changing image names, tags or digests. This can also be achieved with a patch, but this operator is simpler to specify.
  member _.images (value: Image -> Yzl.NamedNode list list) = Yzl.seq "images" (value Image.Default |> Yzl.liftMany)
  member _.inventory (value: Inventory -> Yzl.NamedNode list) = Yzl.map "inventory" (value Inventory.Default)
  member _.kind (value: string) = Yzl.str "kind" value
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  member _.namePrefix (value: string) = Yzl.str "namePrefix" value
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  member _.nameSuffix (value: string) = Yzl.str "nameSuffix" value
  /// Namespace to add to all objects
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  /// Apply a patch to multiple resources
  member _.patches (value: Yzl.Node list) = Yzl.seq "patches" (value |> Yzl.liftMany)
  /// JSONPatches is a list of JSONPatch for applying JSON patch. See http://jsonpatch.com
  member _.patchesJson6902 (value: PatchJson6902 -> Yzl.NamedNode list list) = Yzl.seq "patchesJson6902" (value PatchJson6902.Default |> Yzl.liftMany)
  ///  PatchesStrategicMerge specifies the relative path to a file containing a strategic merge patch. URLs and globs are not supported
  member _.patchesStrategicMerge (value: string list) = Yzl.seq "patchesStrategicMerge" (value |> Yzl.liftMany)
  /// Resources specifies relative paths to files holding YAML representations of kubernetes API objects. URLs and globs not supported.
  member _.resources (value: string list) = Yzl.seq "resources" (value |> Yzl.liftMany)
  /// Components are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file of Kind Component.
  member _.components (value: string list) = Yzl.seq "components" (value |> Yzl.liftMany)
  /// SecretGenerator is a list of secrets to generate from local data (one secret per list item)
  member _.secretGenerator (value: SecretArgs -> Yzl.NamedNode list list) = Yzl.seq "secretGenerator" (value SecretArgs.Default |> Yzl.liftMany)
  /// Transformers is a list of files containing transformers
  member _.transformers (value: string list) = Yzl.seq "transformers" (value |> Yzl.liftMany)
  /// Allows things modified by kustomize to be injected into a container specification. A var is a name (e.g. FOO) associated with a field in a specific resource instance.  The field must contain a value of type string, and defaults to the name field of the instance
  member _.vars (value: Var -> Yzl.NamedNode list list) = Yzl.seq "vars" (value Var.Default |> Yzl.liftMany)
  static member Default = Kustomization()
  static member New (creator:Kustomization -> Yzl.NamedNode list) : Yzl.Node = creator Kustomization.Default |> Yzl.lift

/// NameArgs
type NameArgs() = 
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  static member Default = NameArgs()
  static member New (creator:NameArgs -> Yzl.NamedNode list) : Yzl.Node = creator NameArgs.Default |> Yzl.lift

/// PatchJson6902
type PatchJson6902() = 
  /// relative file path for a json patch file inside a kustomization
  member _.path (value: string) = Yzl.str "path" value
  /// Refers to a Kubernetes object that the json patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  member _.target (value: PatchTarget -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTarget.Default)
  static member Default = PatchJson6902()
  static member New (creator:PatchJson6902 -> Yzl.NamedNode list) : Yzl.Node = creator PatchJson6902.Default |> Yzl.lift

/// PatchTarget
type PatchTarget() = 
  member _.group (value: string) = Yzl.str "group" value
  member _.kind (value: string) = Yzl.str "kind" value
  member _.name (value: string) = Yzl.str "name" value
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  member _.version (value: string) = Yzl.str "version" value
  static member Default = PatchTarget()
  static member New (creator:PatchTarget -> Yzl.NamedNode list) : Yzl.Node = creator PatchTarget.Default |> Yzl.lift

/// PatchTargetOptional
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

/// PatchesPatchPath
type PatchesPatchPath() = 
  member _.path (value: string) = Yzl.str "path" value
  /// Refers to a Kubernetes object that the patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  member _.target (value: PatchTargetOptional -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTargetOptional.Default)
  static member Default = PatchesPatchPath()
  static member New (creator:PatchesPatchPath -> Yzl.NamedNode list) : Yzl.Node = creator PatchesPatchPath.Default |> Yzl.lift

/// PatchesInlinePatch
type PatchesInlinePatch() = 
  member _.patch (value: string) = Yzl.str "patch" value
  /// Refers to a Kubernetes object that the patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  member _.target (value: PatchTargetOptional -> Yzl.NamedNode list) = Yzl.map "target" (value PatchTargetOptional.Default)
  static member Default = PatchesInlinePatch()
  static member New (creator:PatchesInlinePatch -> Yzl.NamedNode list) : Yzl.Node = creator PatchesInlinePatch.Default |> Yzl.lift

/// SecretArgs - SecretArgs contains the metadata of how to generate a secret
type SecretArgs() = 
  member _.kVSources (value: KVSource -> Yzl.NamedNode list list) = Yzl.seq "kVSources" (value KVSource.Default |> Yzl.liftMany)
  member _.behavior (value: string) = Yzl.str "behavior" value
  member _.env (value: string) = Yzl.str "env" value
  member _.envs (value: string list) = Yzl.seq "envs" (value |> Yzl.liftMany)
  member _.files (value: string list) = Yzl.seq "files" (value |> Yzl.liftMany)
  member _.literals (value: string list) = Yzl.seq "literals" (value |> Yzl.liftMany)
  /// Name - actually the partial name - of the generated resource
  member _.name (value: string) = Yzl.str "name" value
  /// Namespace for the secret, optional
  member _.``namespace`` (value: string) = Yzl.str "namespace" value
  /// Type of the secret, optional
  member _.``type`` (value: string) = Yzl.str "type" value
  static member Default = SecretArgs()
  static member New (creator:SecretArgs -> Yzl.NamedNode list) : Yzl.Node = creator SecretArgs.Default |> Yzl.lift

/// Target
type Target() = 
  member _.apiVersion (value: string) = Yzl.str "apiVersion" value
  member _.group (value: string) = Yzl.str "group" value
  member _.kind (value: string) = Yzl.str "kind" value
  member _.name (value: string) = Yzl.str "name" value
  member _.version (value: string) = Yzl.str "version" value
  static member Default = Target()
  static member New (creator:Target -> Yzl.NamedNode list) : Yzl.Node = creator Target.Default |> Yzl.lift

/// Var - Represents a variable whose value will be sourced from a field in a Kubernetes object.
type Var() = 
  /// Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)
  member _.fieldref (value: FieldSelector -> Yzl.NamedNode list) = Yzl.map "fieldref" (value FieldSelector.Default)
  /// Value of identifier name e.g. FOO used in container args, annotations, Appears in pod template as $(FOO)
  member _.name (value: string) = Yzl.str "name" value
  /// Refers to a Kubernetes resource under the purview of this kustomization
  member _.objref (value: Target -> Yzl.NamedNode list) = Yzl.map "objref" (value Target.Default)
  static member Default = Var()
  static member New (creator:Var -> Yzl.NamedNode list) : Yzl.Node = creator Var.Default |> Yzl.lift
