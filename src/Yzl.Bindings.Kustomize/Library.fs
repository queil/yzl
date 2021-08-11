namespace rec Yzl.Bindings.Kustomize
open Yzl.Core

type Common() =
  static member inline name (value: ^a) (_:'b) = Yzl.str "name" value
  static member inline kind (value: ^a) (_:'b) = Yzl.str "kind" value
  static member inline apiVersion (value: ^a) (_:'b) = Yzl.str "apiVersion" value
  static member inline _namespace (value: ^a) (_:'b) = Yzl.str "namespace" value
  

/// ConfigMapArgs - ConfigMapArgs contains the metadata of how to generate a configmap
type ConfigMapArgs() = 
  static member inline kVSources (value: (KVSource -> Yzl.NamedNode) list list) (_:ConfigMapArgs) = Yzl.seq "kVSources" (value |> List.map (fun v -> v |> List.map (fun f -> f KVSource.Default)) |> Yzl.liftMany)
  static member inline behavior (value: string) (_:ConfigMapArgs) = Yzl.str "behavior" value
  /// Deprecated.  Use envs instead.
  static member inline env (value: ^a) (_:ConfigMapArgs) = Yzl.str "env" value
  /// A list of file paths. The contents of each file should be one key=value pair per line
  static member inline envs (value: ^a list) (_:ConfigMapArgs) = Yzl.seq "envs" (value |> Yzl.liftMany)
  /// A list of file sources to use in creating a list of key, value pairs
  static member inline files (value: ^a list) (_:ConfigMapArgs) = Yzl.seq "files" (value |> Yzl.liftMany)
  /// A list of literal pair sources. Each literal source should be a key and literal value, e.g. `key=value`
  static member inline literals (value: ^a list) (_:ConfigMapArgs) = Yzl.seq "literals" (value |> Yzl.liftMany)
  /// Name - actually the partial name - of the generated resource
  static member inline name (value: ^a) (_:ConfigMapArgs) = Yzl.str "name" value
  /// Namespace for the configmap, optional
  static member inline ``namespace`` (value: ^a) (_:ConfigMapArgs) = Yzl.str "namespace" value
  static member Default = ConfigMapArgs()
  static member yzl (build:(ConfigMapArgs -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f ConfigMapArgs.Default) |> Yzl.lift

/// FieldSelector - Contains the fieldPath to an object field
type FieldSelector() = 
  static member inline fieldpath (value: ^a) (_:FieldSelector) = Yzl.str "fieldpath" value
  static member Default = FieldSelector()
  static member yzl (build:(FieldSelector -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f FieldSelector.Default) |> Yzl.lift

/// GeneratorOptions - GeneratorOptions modify behavior of all ConfigMap and Secret generators
type GeneratorOptions() = 
  /// Annotations to add to all generated resources
  static member inline annotations (value: Yzl.NamedNode list) (_:GeneratorOptions) = Yzl.map "annotations" value
  /// DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents
  static member inline disableNameSuffixHash (value: bool) (_:GeneratorOptions) = Yzl.boolean "disableNameSuffixHash" value
  /// Labels to add to all generated resources
  static member inline labels (value: Yzl.NamedNode list) (_:GeneratorOptions) = Yzl.map "labels" value
  static member Default = GeneratorOptions()
  static member yzl (build:(GeneratorOptions -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f GeneratorOptions.Default) |> Yzl.lift

/// Image
type Image() = 
  static member inline digest (value: ^a) (_:Image) = Yzl.str "digest" value
  static member inline name (value: ^a) (_:Image) = Yzl.str "name" value
  static member inline newName (value: ^a) (_:Image) = Yzl.str "newName" value
  static member inline newTag (value: ^a) (_:Image) = Yzl.str "newTag" value
  static member Default = Image()
  static member yzl (build:(Image -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f Image.Default) |> Yzl.lift

/// Inventory - Inventory appends an object that contains the record of all other objects, which can be used in apply, prune and delete
type Inventory() = 
  static member inline configMap (value: (NameArgs -> Yzl.NamedNode) list) (_:Inventory) = Yzl.map "configMap" (value |> List.map (fun f -> f NameArgs.Default))
  static member inline ``type`` (value: ^a) (_:Inventory) = Yzl.str "type" value
  static member Default = Inventory()
  static member yzl (build:(Inventory -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f Inventory.Default) |> Yzl.lift

/// KVSource
type KVSource() = 
  static member inline args (value: ^a list) (_:KVSource) = Yzl.seq "args" (value |> Yzl.liftMany)
  static member inline name (value: ^a) (_:KVSource) = Yzl.str "name" value
  static member inline pluginType (value: ^a) (_:KVSource) = Yzl.str "pluginType" value
  static member Default = KVSource()
  static member yzl (build:(KVSource -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f KVSource.Default) |> Yzl.lift

/// Kustomization
type Kustomization() = 
  static member inline apiVersion (value: ^a) (_:Kustomization) = Yzl.str "apiVersion" value
  /// Bases are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file.
  static member inline bases (value: ^a list) (_:Kustomization) = Yzl.seq "bases" (value |> Yzl.liftMany)
  /// CommonAnnotations to add to all objects
  static member inline commonAnnotations (value: Yzl.NamedNode list) (_:Kustomization) = Yzl.map "commonAnnotations" value
  ///  CommonLabels to add to all objects and selectors
  static member inline commonLabels (value: Yzl.NamedNode list) (_:Kustomization) = Yzl.map "commonLabels" value
  /// ConfigMapGenerator is a list of configmaps to generate from local data (one configMap per list item)
  static member inline configMapGenerator (value: (ConfigMapArgs -> Yzl.NamedNode) list list) (_:Kustomization) = Yzl.seq "configMapGenerator" (value |> List.map (fun v -> v |> List.map (fun f -> f ConfigMapArgs.Default)) |> Yzl.liftMany)
  /// Configurations is a list of transformer configuration files
  static member inline configurations (value: ^a list) (_:Kustomization) = Yzl.seq "configurations" (value |> Yzl.liftMany)
  /// Crds specifies relative paths to Custom Resource Definition files. This allows custom resources to be recognized as operands, making it possible to add them to the Resources list. CRDs themselves are not modified.
  static member inline crds (value: ^a list) (_:Kustomization) = Yzl.seq "crds" (value |> Yzl.liftMany)
  static member inline generatorOptions (value: (GeneratorOptions -> Yzl.NamedNode) list) (_:Kustomization) = Yzl.map "generatorOptions" (value |> List.map (fun f -> f GeneratorOptions.Default))
  /// Generators is a list of files containing custom generators
  static member inline generators (value: ^a list) (_:Kustomization) = Yzl.seq "generators" (value |> Yzl.liftMany)
  /// Images is a list of (image name, new name, new tag or digest) for changing image names, tags or digests. This can also be achieved with a patch, but this operator is simpler to specify.
  static member inline images (value: (Image -> Yzl.NamedNode) list list) (_:Kustomization) = Yzl.seq "images" (value |> List.map (fun v -> v |> List.map (fun f -> f Image.Default)) |> Yzl.liftMany)
  static member inline inventory (value: (Inventory -> Yzl.NamedNode) list) (_:Kustomization) = Yzl.map "inventory" (value |> List.map (fun f -> f Inventory.Default))
  static member inline kind (value: ^a) (_:Kustomization) = Yzl.str "kind" value
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline namePrefix (value: ^a) (_:Kustomization) = Yzl.str "namePrefix" value
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline nameSuffix (value: ^a) (_:Kustomization) = Yzl.str "nameSuffix" value
  /// Namespace to add to all objects
  static member inline ``namespace`` (value: ^a) (_:Kustomization) = Yzl.str "namespace" value
  /// Apply a patch to multiple resources
  static member inline patches (value: Yzl.Node list) (_:Kustomization) = Yzl.seq "patches" (value |> Yzl.liftMany)
  /// JSONPatches is a list of JSONPatch for applying JSON patch. See http://jsonpatch.com
  static member inline patchesJson6902 (value: (PatchJson6902 -> Yzl.NamedNode) list list) (_:Kustomization) = Yzl.seq "patchesJson6902" (value |> List.map (fun v -> v |> List.map (fun f -> f PatchJson6902.Default)) |> Yzl.liftMany)
  ///  PatchesStrategicMerge specifies the relative path to a file containing a strategic merge patch. URLs and globs are not supported
  static member inline patchesStrategicMerge (value: ^a list) (_:Kustomization) = Yzl.seq "patchesStrategicMerge" (value |> Yzl.liftMany)
  /// Resources specifies relative paths to files holding YAML representations of kubernetes API objects. URLs and globs not supported.
  static member inline resources (value: ^a list) (_:Kustomization) = Yzl.seq "resources" (value |> Yzl.liftMany)
  /// Components are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file of Kind Component.
  static member inline components (value: ^a list) (_:Kustomization) = Yzl.seq "components" (value |> Yzl.liftMany)
  /// SecretGenerator is a list of secrets to generate from local data (one secret per list item)
  static member inline secretGenerator (value: (SecretArgs -> Yzl.NamedNode) list list) (_:Kustomization) = Yzl.seq "secretGenerator" (value |> List.map (fun v -> v |> List.map (fun f -> f SecretArgs.Default)) |> Yzl.liftMany)
  /// Transformers is a list of files containing transformers
  static member inline transformers (value: ^a list) (_:Kustomization) = Yzl.seq "transformers" (value |> Yzl.liftMany)
  /// Allows things modified by kustomize to be injected into a container specification. A var is a name (e.g. FOO) associated with a field in a specific resource instance.  The field must contain a value of type string, and defaults to the name field of the instance
  static member inline vars (value: (Var -> Yzl.NamedNode) list list) (_:Kustomization) = Yzl.seq "vars" (value |> List.map (fun v -> v |> List.map (fun f -> f Var.Default)) |> Yzl.liftMany)
  static member Default = Kustomization()
  static member yzl (build:(Kustomization -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f Kustomization.Default) |> Yzl.lift

/// NameArgs
type NameArgs() = 
  static member inline name (value: ^a) (_:NameArgs) = Yzl.str "name" value
  static member inline ``namespace`` (value: ^a) (_:NameArgs) = Yzl.str "namespace" value
  static member Default = NameArgs()
  static member yzl (build:(NameArgs -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f NameArgs.Default) |> Yzl.lift

/// PatchJson6902
type PatchJson6902() = 
  /// relative file path for a json patch file inside a kustomization
  static member inline path (value: ^a) (_:PatchJson6902) = Yzl.str "path" value
  /// Refers to a Kubernetes object that the json patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  static member inline target (value: (PatchTarget -> Yzl.NamedNode) list) (_:PatchJson6902) = Yzl.map "target" (value |> List.map (fun f -> f PatchTarget.Default))
  static member Default = PatchJson6902()
  static member yzl (build:(PatchJson6902 -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f PatchJson6902.Default) |> Yzl.lift

/// PatchTarget
type PatchTarget() = 
  static member inline group (value: ^a) (_:PatchTarget) = Yzl.str "group" value
  static member inline kind (value: ^a) (_:PatchTarget) = Yzl.str "kind" value
  static member inline name (value: ^a) (_:PatchTarget) = Yzl.str "name" value
  static member inline ``namespace`` (value: ^a) (_:PatchTarget) = Yzl.str "namespace" value
  static member inline version (value: ^a) (_:PatchTarget) = Yzl.str "version" value
  static member Default = PatchTarget()
  static member yzl (build:(PatchTarget -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f PatchTarget.Default) |> Yzl.lift

/// PatchTargetOptional
type PatchTargetOptional() = 
  static member inline group (value: ^a) (_:PatchTargetOptional) = Yzl.str "group" value
  static member inline kind (value: ^a) (_:PatchTargetOptional) = Yzl.str "kind" value
  static member inline name (value: ^a) (_:PatchTargetOptional) = Yzl.str "name" value
  static member inline ``namespace`` (value: ^a) (_:PatchTargetOptional) = Yzl.str "namespace" value
  static member inline version (value: ^a) (_:PatchTargetOptional) = Yzl.str "version" value
  static member inline labelSelector (value: ^a) (_:PatchTargetOptional) = Yzl.str "labelSelector" value
  static member inline annotationSelector (value: ^a) (_:PatchTargetOptional) = Yzl.str "annotationSelector" value
  static member Default = PatchTargetOptional()
  static member yzl (build:(PatchTargetOptional -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f PatchTargetOptional.Default) |> Yzl.lift

/// PatchesPatchPath
type PatchesPatchPath() = 
  static member inline path (value: ^a) (_:PatchesPatchPath) = Yzl.str "path" value
  /// Refers to a Kubernetes object that the patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  static member inline target (value: (PatchTargetOptional -> Yzl.NamedNode) list) (_:PatchesPatchPath) = Yzl.map "target" (value |> List.map (fun f -> f PatchTargetOptional.Default))
  static member Default = PatchesPatchPath()
  static member yzl (build:(PatchesPatchPath -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f PatchesPatchPath.Default) |> Yzl.lift

/// PatchesInlinePatch
type PatchesInlinePatch() = 
  static member inline patch (value: ^a) (_:PatchesInlinePatch) = Yzl.str "patch" value
  /// Refers to a Kubernetes object that the patch will be applied to. It must refer to a Kubernetes resource under the purview of this kustomization
  static member inline target (value: (PatchTargetOptional -> Yzl.NamedNode) list) (_:PatchesInlinePatch) = Yzl.map "target" (value |> List.map (fun f -> f PatchTargetOptional.Default))
  static member Default = PatchesInlinePatch()
  static member yzl (build:(PatchesInlinePatch -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f PatchesInlinePatch.Default) |> Yzl.lift

/// SecretArgs - SecretArgs contains the metadata of how to generate a secret
type SecretArgs() = 
  static member inline kVSources (value: (KVSource -> Yzl.NamedNode) list list) (_:SecretArgs) = Yzl.seq "kVSources" (value |> List.map (fun v -> v |> List.map (fun f -> f KVSource.Default)) |> Yzl.liftMany)
  static member inline behavior (value: string) (_:SecretArgs) = Yzl.str "behavior" value
  static member inline env (value: ^a) (_:SecretArgs) = Yzl.str "env" value
  static member inline envs (value: ^a list) (_:SecretArgs) = Yzl.seq "envs" (value |> Yzl.liftMany)
  static member inline files (value: ^a list) (_:SecretArgs) = Yzl.seq "files" (value |> Yzl.liftMany)
  static member inline literals (value: ^a list) (_:SecretArgs) = Yzl.seq "literals" (value |> Yzl.liftMany)
  /// Name - actually the partial name - of the generated resource
  static member inline name (value: ^a) (_:SecretArgs) = Yzl.str "name" value
  /// Namespace for the secret, optional
  static member inline ``namespace`` (value: ^a) (_:SecretArgs) = Yzl.str "namespace" value
  /// Type of the secret, optional
  static member inline ``type`` (value: ^a) (_:SecretArgs) = Yzl.str "type" value
  static member Default = SecretArgs()
  static member yzl (build:(SecretArgs -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f SecretArgs.Default) |> Yzl.lift

/// Target
type Target() = 
  static member inline apiVersion (value: ^a) (_:Target) = Yzl.str "apiVersion" value
  static member inline group (value: ^a) (_:Target) = Yzl.str "group" value
  static member inline kind (value: ^a) (_:Target) = Yzl.str "kind" value
  static member inline name (value: ^a) (_:Target) = Yzl.str "name" value
  static member inline version (value: ^a) (_:Target) = Yzl.str "version" value
  static member Default = Target()
  static member yzl (build:(Target -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f Target.Default) |> Yzl.lift

/// Var - Represents a variable whose value will be sourced from a field in a Kubernetes object.
type Var() = 
  /// Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)
  static member inline fieldref (value: (FieldSelector -> Yzl.NamedNode) list) (_:Var) = Yzl.map "fieldref" (value |> List.map (fun f -> f FieldSelector.Default))
  /// Value of identifier name e.g. FOO used in container args, annotations, Appears in pod template as $(FOO)
  static member inline name (value: ^a) (_:Var) = Yzl.str "name" value
  /// Refers to a Kubernetes resource under the purview of this kustomization
  static member inline objref (value: (Target -> Yzl.NamedNode) list) (_:Var) = Yzl.map "objref" (value |> List.map (fun f -> f Target.Default))
  static member Default = Var()
  static member yzl (build:(Var -> Yzl.NamedNode) list) : Yzl.Node = build |> List.map (fun f -> f Var.Default) |> Yzl.lift
