namespace rec Yzl.Bindings.Kustomize
open Yzl.Core
/// Common tags
type Common() =
  static member inline name (value: string)  = Yzl.str "name" value
  static member inline name (value: Yzl.Str)  = Yzl.str "name" value
  static member inline version (value: string)  = Yzl.str "version" value
  static member inline version (value: Yzl.Str)  = Yzl.str "version" value
  static member inline kind (value: string)  = Yzl.str "kind" value
  static member inline kind (value: Yzl.Str)  = Yzl.str "kind" value
  static member inline group (value: string)  = Yzl.str "group" value
  static member inline group (value: Yzl.Str)  = Yzl.str "group" value
  static member inline apiVersion (value: string)  = Yzl.str "apiVersion" value
  static member inline apiVersion (value: Yzl.Str)  = Yzl.str "apiVersion" value
  static member inline ``type`` (value: string)  = Yzl.str "type" value
  static member inline ``type`` (value: Yzl.Str)  = Yzl.str "type" value
  static member inline options (value: Yzl.NamedNode list)  = Yzl.map "options" value
  static member inline ``namespace`` (value: string)  = Yzl.str "namespace" value
  static member inline ``namespace`` (value: Yzl.Str)  = Yzl.str "namespace" value
  static member inline literals (value: string list)  = Yzl.seq "literals" (value |> Yzl.liftMany)
  static member inline files (value: string list)  = Yzl.seq "files" (value |> Yzl.liftMany)
  static member inline envs (value: string list)  = Yzl.seq "envs" (value |> Yzl.liftMany)
  static member inline env (value: string)  = Yzl.str "env" value
  static member inline env (value: Yzl.Str)  = Yzl.str "env" value
  static member inline behavior (value: string)  = Yzl.str "behavior" value
  static member inline KVSources (value: Yzl.NamedNode list list)  = Yzl.seq "KVSources" (value |> Yzl.liftMany)
  static member inline target (value: Yzl.NamedNode list)  = Yzl.map "target" value
  static member Default = Common()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Represents a variable whose value will be sourced from a field in a Kubernetes object.
type Var() =
  /// Refers to a Kubernetes resource under the purview of this kustomization
  static member inline objref (value: Yzl.NamedNode list)  = Yzl.map "objref" value
  /// Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)
  static member inline fieldref (value: Yzl.NamedNode list)  = Yzl.map "fieldref" value
  static member Default = Var()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Target() =
  static member Default = Target()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// SecretArgs contains the metadata of how to generate a secret
type SecretArgs() =
  static member Default = SecretArgs()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Replicas() =
  static member inline count (value: float)  = Yzl.float "count" value
  static member Default = Replicas()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchesInlinePatch() =
  static member inline patch (value: string)  = Yzl.str "patch" value
  static member inline patch (value: Yzl.Str)  = Yzl.str "patch" value
  static member Default = PatchesInlinePatch()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchesPatchPath() =
  static member inline path (value: string)  = Yzl.str "path" value
  static member inline path (value: Yzl.Str)  = Yzl.str "path" value
  static member Default = PatchesPatchPath()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchTargetOptional() =
  static member inline annotationSelector (value: string)  = Yzl.str "annotationSelector" value
  static member inline annotationSelector (value: Yzl.Str)  = Yzl.str "annotationSelector" value
  static member inline labelSelector (value: string)  = Yzl.str "labelSelector" value
  static member inline labelSelector (value: Yzl.Str)  = Yzl.str "labelSelector" value
  static member Default = PatchTargetOptional()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchTarget() =
  static member Default = PatchTarget()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchJson6902() =
  static member Default = PatchJson6902()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type NameArgs() =
  static member Default = NameArgs()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Kustomization() =
  /// Allows things modified by kustomize to be injected into a container specification. A var is a name (e.g. FOO) associated with a field in a specific resource instance.  The field must contain a value of type string, and defaults to the name field of the instance
  static member inline vars (value: Yzl.NamedNode list list)  = Yzl.seq "vars" (value |> Yzl.liftMany)
  /// Transformers is a list of files containing transformers
  static member inline transformers (value: string list)  = Yzl.seq "transformers" (value |> Yzl.liftMany)
  /// SecretGenerator is a list of secrets to generate from local data (one secret per list item)
  static member inline secretGenerator (value: Yzl.NamedNode list list)  = Yzl.seq "secretGenerator" (value |> Yzl.liftMany)
  /// Components are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file of Kind Component.
  static member inline components (value: string list)  = Yzl.seq "components" (value |> Yzl.liftMany)
  /// Resources specifies relative paths to files holding YAML representations of kubernetes API objects. URLs and globs not supported.
  static member inline resources (value: string list)  = Yzl.seq "resources" (value |> Yzl.liftMany)
  /// Replicas is a list of (resource name, count) for changing number of replicas for a resources. It will match any group and kind that has a matching name and that is one of: Deployment, ReplicationController, Replicaset, Statefulset.
  static member inline replicas (value: Yzl.NamedNode list list)  = Yzl.seq "replicas" (value |> Yzl.liftMany)
  ///  PatchesStrategicMerge specifies the relative path to a file containing a strategic merge patch. URLs and globs are not supported
  static member inline patchesStrategicMerge (value: string list)  = Yzl.seq "patchesStrategicMerge" (value |> Yzl.liftMany)
  /// JSONPatches is a list of JSONPatch for applying JSON patch. See http://jsonpatch.com
  static member inline patchesJson6902 (value: Yzl.NamedNode list list)  = Yzl.seq "patchesJson6902" (value |> Yzl.liftMany)
  /// Apply a patch to multiple resources
  static member inline patches (value: Yzl.Node list)  = Yzl.seq "patches" (value |> Yzl.liftMany)
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline nameSuffix (value: string)  = Yzl.str "nameSuffix" value
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline nameSuffix (value: Yzl.Str)  = Yzl.str "nameSuffix" value
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline namePrefix (value: string)  = Yzl.str "namePrefix" value
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member inline namePrefix (value: Yzl.Str)  = Yzl.str "namePrefix" value
  static member inline inventory (value: Yzl.NamedNode list)  = Yzl.map "inventory" value
  /// Images is a list of (image name, new name, new tag or digest) for changing image names, tags or digests. This can also be achieved with a patch, but this operator is simpler to specify.
  static member inline images (value: Yzl.NamedNode list list)  = Yzl.seq "images" (value |> Yzl.liftMany)
  /// Generators is a list of files containing custom generators
  static member inline generators (value: string list)  = Yzl.seq "generators" (value |> Yzl.liftMany)
  static member inline generatorOptions (value: Yzl.NamedNode list)  = Yzl.map "generatorOptions" value
  /// Crds specifies relative paths to Custom Resource Definition files. This allows custom resources to be recognized as operands, making it possible to add them to the Resources list. CRDs themselves are not modified.
  static member inline crds (value: string list)  = Yzl.seq "crds" (value |> Yzl.liftMany)
  /// Configurations is a list of transformer configuration files
  static member inline configurations (value: string list)  = Yzl.seq "configurations" (value |> Yzl.liftMany)
  /// ConfigMapGenerator is a list of configmaps to generate from local data (one configMap per list item)
  static member inline configMapGenerator (value: Yzl.NamedNode list list)  = Yzl.seq "configMapGenerator" (value |> Yzl.liftMany)
  ///  CommonLabels to add to all objects and selectors
  static member inline commonLabels (value: Yzl.NamedNode list)  = Yzl.map "commonLabels" value
  /// CommonAnnotations to add to all objects
  static member inline commonAnnotations (value: Yzl.NamedNode list)  = Yzl.map "commonAnnotations" value
  /// Bases are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file.
  static member inline bases (value: string list)  = Yzl.seq "bases" (value |> Yzl.liftMany)
  static member Default = Kustomization()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type KVSource() =
  static member inline pluginType (value: string)  = Yzl.str "pluginType" value
  static member inline pluginType (value: Yzl.Str)  = Yzl.str "pluginType" value
  static member inline args (value: string list)  = Yzl.seq "args" (value |> Yzl.liftMany)
  static member Default = KVSource()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Inventory appends an object that contains the record of all other objects, which can be used in apply, prune and delete
type Inventory() =
  static member inline configMap (value: Yzl.NamedNode list)  = Yzl.map "configMap" value
  static member Default = Inventory()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Image() =
  static member inline newTag (value: string)  = Yzl.str "newTag" value
  static member inline newTag (value: Yzl.Str)  = Yzl.str "newTag" value
  static member inline newName (value: string)  = Yzl.str "newName" value
  static member inline newName (value: Yzl.Str)  = Yzl.str "newName" value
  static member inline digest (value: string)  = Yzl.str "digest" value
  static member inline digest (value: Yzl.Str)  = Yzl.str "digest" value
  static member Default = Image()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// GeneratorOptions modify behavior of all ConfigMap and Secret generators
type GeneratorOptions() =
  /// Labels to add to all generated resources
  static member inline labels (value: Yzl.NamedNode list)  = Yzl.map "labels" value
  /// DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents
  static member inline disableNameSuffixHash (value: bool)  = Yzl.boolean "disableNameSuffixHash" value
  /// Annotations to add to all generated resources
  static member inline annotations (value: Yzl.NamedNode list)  = Yzl.map "annotations" value
  static member Default = GeneratorOptions()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Contains the fieldPath to an object field
type FieldSelector() =
  static member inline fieldpath (value: string)  = Yzl.str "fieldpath" value
  static member inline fieldpath (value: Yzl.Str)  = Yzl.str "fieldpath" value
  static member Default = FieldSelector()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// ConfigMapArgs contains the metadata of how to generate a configmap
type ConfigMapArgs() =
  static member Default = ConfigMapArgs()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift

