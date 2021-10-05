namespace rec Yzl.Bindings.Kustomize
open Yzl.Core
/// Common tags
type Common() =
  static member name (value: string)  = Yzl.Builder.str value "name"
  static member name (value: Yzl.Str)  = Yzl.Builder.str value "name"
  static member version (value: string)  = Yzl.Builder.str value "version"
  static member version (value: Yzl.Str)  = Yzl.Builder.str value "version"
  static member kind (value: string)  = Yzl.Builder.str value "kind"
  static member kind (value: Yzl.Str)  = Yzl.Builder.str value "kind"
  static member group (value: string)  = Yzl.Builder.str value "group"
  static member group (value: Yzl.Str)  = Yzl.Builder.str value "group"
  static member apiVersion (value: string)  = Yzl.Builder.str value "apiVersion"
  static member apiVersion (value: Yzl.Str)  = Yzl.Builder.str value "apiVersion"
  static member ``type`` (value: string)  = Yzl.Builder.str value "type"
  static member ``type`` (value: Yzl.Str)  = Yzl.Builder.str value "type"
  static member options (value: Yzl.NamedNode list)  = Yzl.Builder.map value "options"
  static member ``namespace`` (value: string)  = Yzl.Builder.str value "namespace"
  static member ``namespace`` (value: Yzl.Str)  = Yzl.Builder.str value "namespace"
  static member literals (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "literals"
  static member files (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "files"
  static member envs (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "envs"
  static member env (value: string)  = Yzl.Builder.str value "env"
  static member env (value: Yzl.Str)  = Yzl.Builder.str value "env"
  static member behavior (value: string)  = Yzl.Builder.str value "behavior"
  static member KVSources (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "KVSources"
  static member path (value: string)  = Yzl.Builder.str value "path"
  static member path (value: Yzl.Str)  = Yzl.Builder.str value "path"
  static member target (value: Yzl.NamedNode list)  = Yzl.Builder.map value "target"
  static member annotations (value: Yzl.NamedNode list)  = Yzl.Builder.map value "annotations"
  static member labels (value: Yzl.NamedNode list)  = Yzl.Builder.map value "labels"
  static member Default = Common()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Represents a variable whose value will be sourced from a field in a Kubernetes object.
type Var() =
  /// Refers to a Kubernetes resource under the purview of this kustomization
  static member objref (value: Yzl.NamedNode list)  = Yzl.Builder.map value "objref"
  /// Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)
  static member fieldref (value: Yzl.NamedNode list)  = Yzl.Builder.map value "fieldref"
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
  static member count (value: float)  = Yzl.Builder.float value "count"
  static member Default = Replicas()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type ReplacementsPath() =
  static member Default = ReplacementsPath()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type ReplacementsInline() =
  /// The N fields to write the value to
  static member targets (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "targets"
  /// The source of the value
  static member source (value: Yzl.NamedNode list)  = Yzl.Builder.map value "source"
  static member Default = ReplacementsInline()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchesInlinePatch() =
  static member patch (value: string)  = Yzl.Builder.str value "patch"
  static member patch (value: Yzl.Str)  = Yzl.Builder.str value "patch"
  static member Default = PatchesInlinePatch()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchesPatchPath() =
  static member Default = PatchesPatchPath()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type PatchTargetOptional() =
  static member annotationSelector (value: string)  = Yzl.Builder.str value "annotationSelector"
  static member annotationSelector (value: Yzl.Str)  = Yzl.Builder.str value "annotationSelector"
  static member labelSelector (value: string)  = Yzl.Builder.str value "labelSelector"
  static member labelSelector (value: Yzl.Str)  = Yzl.Builder.str value "labelSelector"
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
type Metadata() =
  static member Default = Metadata()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Labels() =
  /// FieldSpec completely specifies a kustomizable field in a k8s API object. It helps define the operands of transformations
  static member fields (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "fields"
  /// IncludeSelectors inidicates should transformer include the fieldSpecs for selectors
  static member includeSelectors (value: bool)  = Yzl.Builder.boolean value "includeSelectors"
  /// Pairs contains the key-value pairs for labels to add
  static member pairs (value: Yzl.NamedNode list)  = Yzl.Builder.map value "pairs"
  static member Default = Labels()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Kustomization() =
  /// Allows things modified by kustomize to be injected into a container specification. A var is a name (e.g. FOO) associated with a field in a specific resource instance.  The field must contain a value of type string, and defaults to the name field of the instance
  static member vars (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "vars"
  /// Validators is a list of files containing validators
  static member validators (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "validators"
  /// Transformers is a list of files containing transformers
  static member transformers (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "transformers"
  /// SecretGenerator is a list of secrets to generate from local data (one secret per list item)
  static member secretGenerator (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "secretGenerator"
  /// Components are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file of Kind Component.
  static member components (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "components"
  /// Resources specifies relative paths to files holding YAML representations of kubernetes API objects. URLs and globs not supported.
  static member resources (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "resources"
  /// Replicas is a list of (resource name, count) for changing number of replicas for a resources. It will match any group and kind that has a matching name and that is one of: Deployment, ReplicationController, Replicaset, Statefulset.
  static member replicas (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "replicas"
  ///  PatchesStrategicMerge specifies the relative path to a file containing a strategic merge patch. URLs and globs are not supported
  static member patchesStrategicMerge (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "patchesStrategicMerge"
  /// JSONPatches is a list of JSONPatch for applying JSON patch. See http://jsonpatch.com
  static member patchesJson6902 (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "patchesJson6902"
  /// Apply a patch to multiple resources
  static member patches (value: Yzl.Node list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "patches"
  /// OpenAPI contains information about what kubernetes schema to use
  static member openapi (value: Yzl.NamedNode list)  = Yzl.Builder.map value "openapi"
  /// Substitute field(s) in N target(s) with a field from a source
  static member replacements (value: Yzl.Node list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "replacements"
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member nameSuffix (value: string)  = Yzl.Builder.str value "nameSuffix"
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member nameSuffix (value: Yzl.Str)  = Yzl.Builder.str value "nameSuffix"
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member namePrefix (value: string)  = Yzl.Builder.str value "namePrefix"
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member namePrefix (value: Yzl.Str)  = Yzl.Builder.str value "namePrefix"
  /// Contains metadata about a Resource
  static member metadata (value: Yzl.NamedNode list)  = Yzl.Builder.map value "metadata"
  /// Labels to add to all objects but not selectors
  static member labels (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "labels"
  static member inventory (value: Yzl.NamedNode list)  = Yzl.Builder.map value "inventory"
  /// Images is a list of (image name, new name, new tag or digest) for changing image names, tags or digests. This can also be achieved with a patch, but this operator is simpler to specify.
  static member images (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "images"
  /// HelmGlobals contains helm configuration that isn't chart specific
  static member helmGlobals (value: Yzl.NamedNode list)  = Yzl.Builder.map value "helmGlobals"
  /// HelmCharts is a list of helm chart configuration instances
  static member helmCharts (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "helmCharts"
  /// Generators is a list of files containing custom generators
  static member generators (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "generators"
  static member generatorOptions (value: Yzl.NamedNode list)  = Yzl.Builder.map value "generatorOptions"
  /// Crds specifies relative paths to Custom Resource Definition files. This allows custom resources to be recognized as operands, making it possible to add them to the Resources list. CRDs themselves are not modified.
  static member crds (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "crds"
  /// Configurations is a list of transformer configuration files
  static member configurations (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "configurations"
  /// ConfigMapGenerator is a list of configmaps to generate from local data (one configMap per list item)
  static member configMapGenerator (value: Yzl.NamedNode list list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "configMapGenerator"
  ///  CommonLabels to add to all objects and selectors
  static member commonLabels (value: Yzl.NamedNode list)  = Yzl.Builder.map value "commonLabels"
  /// BuildMetadata is a list of strings used to toggle different build options
  static member buildMetadata (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "buildMetadata"
  /// CommonAnnotations to add to all objects
  static member commonAnnotations (value: Yzl.NamedNode list)  = Yzl.Builder.map value "commonAnnotations"
  /// DEPRECATED. Bases are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file.
  static member bases (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "bases"
  static member Default = Kustomization()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type KVSource() =
  static member pluginType (value: string)  = Yzl.Builder.str value "pluginType"
  static member pluginType (value: Yzl.Str)  = Yzl.Builder.str value "pluginType"
  static member args (value: string list)  = Yzl.Builder.seq (value |> Yzl.liftMany) "args"
  static member Default = KVSource()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Inventory appends an object that contains the record of all other objects, which can be used in apply, prune and delete
type Inventory() =
  static member configMap (value: Yzl.NamedNode list)  = Yzl.Builder.map value "configMap"
  static member Default = Inventory()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type Image() =
  static member newTag (value: string)  = Yzl.Builder.str value "newTag"
  static member newTag (value: Yzl.Str)  = Yzl.Builder.str value "newTag"
  static member newName (value: string)  = Yzl.Builder.str value "newName"
  static member newName (value: Yzl.Str)  = Yzl.Builder.str value "newName"
  static member digest (value: string)  = Yzl.Builder.str value "digest"
  static member digest (value: Yzl.Str)  = Yzl.Builder.str value "digest"
  static member Default = Image()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type HelmChart() =
  static member includeCRDs (value: bool)  = Yzl.Builder.boolean value "includeCRDs"
  static member valuesMerge (value: string)  = Yzl.Builder.str value "valuesMerge"
  static member valuesMerge (value: Yzl.Str)  = Yzl.Builder.str value "valuesMerge"
  static member valuesInline (value: Yzl.NamedNode list)  = Yzl.Builder.map value "valuesInline"
  static member valuesFile (value: string)  = Yzl.Builder.str value "valuesFile"
  static member valuesFile (value: Yzl.Str)  = Yzl.Builder.str value "valuesFile"
  static member releaseName (value: string)  = Yzl.Builder.str value "releaseName"
  static member releaseName (value: Yzl.Str)  = Yzl.Builder.str value "releaseName"
  static member repo (value: string)  = Yzl.Builder.str value "repo"
  static member repo (value: Yzl.Str)  = Yzl.Builder.str value "repo"
  static member Default = HelmChart()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// GeneratorOptions modify behavior of all ConfigMap and Secret generators
type GeneratorOptions() =
  /// Immutable if true add to all generated resources
  static member immutable (value: bool)  = Yzl.Builder.boolean value "immutable"
  /// DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents
  static member disableNameSuffixHash (value: bool)  = Yzl.Builder.boolean value "disableNameSuffixHash"
  static member Default = GeneratorOptions()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
type FieldSpec() =
  static member create (value: bool)  = Yzl.Builder.boolean value "create"
  static member Default = FieldSpec()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// Contains the fieldPath to an object field
type FieldSelector() =
  static member fieldpath (value: string)  = Yzl.Builder.str value "fieldpath"
  static member fieldpath (value: Yzl.Str)  = Yzl.Builder.str value "fieldpath"
  static member Default = FieldSelector()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift
/// ConfigMapArgs contains the metadata of how to generate a configmap
type ConfigMapArgs() =
  static member Default = ConfigMapArgs()
  static member yzl (build:Yzl.NamedNode list) : Yzl.Node = build |> Yzl.lift

