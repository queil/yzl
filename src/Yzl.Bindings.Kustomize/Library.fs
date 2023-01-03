namespace rec Yzl.Bindings.Kustomize
open Yzl.Core
/// Common tags
type Common() =
  static member name (value: string)  = Yzl.str(value, "name")
  static member name (value: Str)  = Yzl.str(value, "name")
  static member version (value: string)  = Yzl.str(value, "version")
  static member version (value: Str)  = Yzl.str(value, "version")
  static member kind (value: string)  = Yzl.str(value, "kind")
  static member kind (value: Str)  = Yzl.str(value, "kind")
  static member group (value: string)  = Yzl.str(value, "group")
  static member group (value: Str)  = Yzl.str(value, "group")
  static member apiVersion (value: string)  = Yzl.str(value, "apiVersion")
  static member apiVersion (value: Str)  = Yzl.str(value, "apiVersion")
  static member ``type`` (value: string)  = Yzl.str(value, "type")
  static member ``type`` (value: Str)  = Yzl.str(value, "type")
  static member options (value: NamedNode list)  = Yzl.map(value, "options")
  static member ``namespace`` (value: string)  = Yzl.str(value, "namespace")
  static member ``namespace`` (value: Str)  = Yzl.str(value, "namespace")
  static member literals (value: string list)  = Yzl.seq(value, "literals")
  static member files (value: string list)  = Yzl.seq(value, "files")
  static member envs (value: string list)  = Yzl.seq(value, "envs")
  static member env (value: string)  = Yzl.str(value, "env")
  static member env (value: Str)  = Yzl.str(value, "env")
  static member behavior (value: string)  = Yzl.str(value, "behavior")
  static member KVSources (value: NamedNode list list)  = Yzl.seq(value, "KVSources")
  static member path (value: string)  = Yzl.str(value, "path")
  static member path (value: Str)  = Yzl.str(value, "path")
  static member target (value: NamedNode list)  = Yzl.map(value, "target")
  static member annotations (value: NamedNode list)  = Yzl.map(value, "annotations")
  static member labels (value: NamedNode list)  = Yzl.map(value, "labels")
  static member Default = Common()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// Represents a variable whose value will be sourced from a field in a Kubernetes object.
type Var() =
  /// Refers to a Kubernetes resource under the purview of this kustomization
  static member objref (value: NamedNode list)  = Yzl.map(value, "objref")
  /// Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)
  static member fieldref (value: NamedNode list)  = Yzl.map(value, "fieldref")
  static member Default = Var()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Target() =
  static member Default = Target()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// SecretArgs contains the metadata of how to generate a secret
type SecretArgs() =
  static member Default = SecretArgs()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Replicas() =
  static member count (value: float)  = Yzl.float(value, "count")
  static member Default = Replicas()
  static member yzl (build:NamedNode list) : Node = build |> lift
type ReplacementsPath() =
  static member Default = ReplacementsPath()
  static member yzl (build:NamedNode list) : Node = build |> lift
type ReplacementsInline() =
  /// The N fields to write the value to
  static member targets (value: NamedNode list list)  = Yzl.seq(value, "targets")
  /// The source of the value
  static member source (value: NamedNode list)  = Yzl.map(value, "source")
  static member Default = ReplacementsInline()
  static member yzl (build:NamedNode list) : Node = build |> lift
type PatchesInlinePatch() =
  static member patch (value: string)  = Yzl.str(value, "patch")
  static member patch (value: Str)  = Yzl.str(value, "patch")
  static member Default = PatchesInlinePatch()
  static member yzl (build:NamedNode list) : Node = build |> lift
type PatchesPatchPath() =
  static member Default = PatchesPatchPath()
  static member yzl (build:NamedNode list) : Node = build |> lift
type PatchTargetOptional() =
  static member annotationSelector (value: string)  = Yzl.str(value, "annotationSelector")
  static member annotationSelector (value: Str)  = Yzl.str(value, "annotationSelector")
  static member labelSelector (value: string)  = Yzl.str(value, "labelSelector")
  static member labelSelector (value: Str)  = Yzl.str(value, "labelSelector")
  static member Default = PatchTargetOptional()
  static member yzl (build:NamedNode list) : Node = build |> lift
type PatchTarget() =
  static member Default = PatchTarget()
  static member yzl (build:NamedNode list) : Node = build |> lift
type PatchJson6902() =
  static member Default = PatchJson6902()
  static member yzl (build:NamedNode list) : Node = build |> lift
type NameArgs() =
  static member Default = NameArgs()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Metadata() =
  static member Default = Metadata()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Labels() =
  /// FieldSpec completely specifies a kustomizable field in a k8s API object. It helps define the operands of transformations
  static member fields (value: NamedNode list list)  = Yzl.seq(value, "fields")
  /// IncludeTemplates inidicates should transformer include the template labels
  static member includeTemplates (value: bool)  = Yzl.boolean(value, "includeTemplates")
  /// IncludeSelectors inidicates should transformer include the fieldSpecs for selectors
  static member includeSelectors (value: bool)  = Yzl.boolean(value, "includeSelectors")
  /// Pairs contains the key-value pairs for labels to add
  static member pairs (value: NamedNode list)  = Yzl.map(value, "pairs")
  static member Default = Labels()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Kustomization() =
  /// Allows things modified by kustomize to be injected into a container specification. A var is a name (e.g. FOO) associated with a field in a specific resource instance.  The field must contain a value of type string, and defaults to the name field of the instance
  static member vars (value: NamedNode list list)  = Yzl.seq(value, "vars")
  /// Validators is a list of files containing validators
  static member validators (value: string list)  = Yzl.seq(value, "validators")
  /// Transformers is a list of files containing transformers
  static member transformers (value: string list)  = Yzl.seq(value, "transformers")
  /// SecretGenerator is a list of secrets to generate from local data (one secret per list item)
  static member secretGenerator (value: NamedNode list list)  = Yzl.seq(value, "secretGenerator")
  /// Components are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file of Kind Component.
  static member components (value: string list)  = Yzl.seq(value, "components")
  /// Resources specifies relative paths to files holding YAML representations of kubernetes API objects. URLs and globs not supported.
  static member resources (value: string list)  = Yzl.seq(value, "resources")
  /// Replicas is a list of (resource name, count) for changing number of replicas for a resources. It will match any group and kind that has a matching name and that is one of: Deployment, ReplicationController, Replicaset, Statefulset.
  static member replicas (value: NamedNode list list)  = Yzl.seq(value, "replicas")
  ///  PatchesStrategicMerge specifies the relative path to a file containing a strategic merge patch. URLs and globs are not supported
  static member patchesStrategicMerge (value: string list)  = Yzl.seq(value, "patchesStrategicMerge")
  /// JSONPatches is a list of JSONPatch for applying JSON patch. See http://jsonpatch.com
  static member patchesJson6902 (value: NamedNode list list)  = Yzl.seq(value, "patchesJson6902")
  /// Apply a patch to multiple resources
  static member patches (value: Node list)  = Yzl.seq(value, "patches")
  /// OpenAPI contains information about what kubernetes schema to use
  static member openapi (value: NamedNode list)  = Yzl.map(value, "openapi")
  /// Substitute field(s) in N target(s) with a field from a source
  static member replacements (value: Node list)  = Yzl.seq(value, "replacements")
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member nameSuffix (value: string)  = Yzl.str(value, "nameSuffix")
  /// NameSuffix will suffix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member nameSuffix (value: Str)  = Yzl.str(value, "nameSuffix")
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member namePrefix (value: string)  = Yzl.str(value, "namePrefix")
  /// NamePrefix will prefix the names of all resources mentioned in the kustomization file including generated configmaps and secrets
  static member namePrefix (value: Str)  = Yzl.str(value, "namePrefix")
  /// Contains metadata about a Resource
  static member metadata (value: NamedNode list)  = Yzl.map(value, "metadata")
  /// Labels to add to all objects but not selectors
  static member labels (value: NamedNode list list)  = Yzl.seq(value, "labels")
  static member inventory (value: NamedNode list)  = Yzl.map(value, "inventory")
  /// Images is a list of (image name, new name, new tag or digest) for changing image names, tags or digests. This can also be achieved with a patch, but this operator is simpler to specify.
  static member images (value: NamedNode list list)  = Yzl.seq(value, "images")
  /// HelmGlobals contains helm configuration that isn't chart specific
  static member helmGlobals (value: NamedNode list)  = Yzl.map(value, "helmGlobals")
  /// HelmCharts is a list of helm chart configuration instances
  static member helmCharts (value: NamedNode list list)  = Yzl.seq(value, "helmCharts")
  /// Generators is a list of files containing custom generators
  static member generators (value: string list)  = Yzl.seq(value, "generators")
  static member generatorOptions (value: NamedNode list)  = Yzl.map(value, "generatorOptions")
  /// Crds specifies relative paths to Custom Resource Definition files. This allows custom resources to be recognized as operands, making it possible to add them to the Resources list. CRDs themselves are not modified.
  static member crds (value: string list)  = Yzl.seq(value, "crds")
  /// Configurations is a list of transformer configuration files
  static member configurations (value: string list)  = Yzl.seq(value, "configurations")
  /// ConfigMapGenerator is a list of configmaps to generate from local data (one configMap per list item)
  static member configMapGenerator (value: NamedNode list list)  = Yzl.seq(value, "configMapGenerator")
  ///  CommonLabels to add to all objects and selectors
  static member commonLabels (value: NamedNode list)  = Yzl.map(value, "commonLabels")
  /// BuildMetadata is a list of strings used to toggle different build options
  static member buildMetadata (value: string list)  = Yzl.seq(value, "buildMetadata")
  /// CommonAnnotations to add to all objects
  static member commonAnnotations (value: NamedNode list)  = Yzl.map(value, "commonAnnotations")
  /// DEPRECATED. Bases are relative paths or git repository URLs specifying a directory containing a kustomization.yaml file.
  static member bases (value: string list)  = Yzl.seq(value, "bases")
  static member Default = Kustomization()
  static member yzl (build:NamedNode list) : Node = build |> lift
type KVSource() =
  static member pluginType (value: string)  = Yzl.str(value, "pluginType")
  static member pluginType (value: Str)  = Yzl.str(value, "pluginType")
  static member args (value: string list)  = Yzl.seq(value, "args")
  static member Default = KVSource()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// Inventory appends an object that contains the record of all other objects, which can be used in apply, prune and delete
type Inventory() =
  static member configMap (value: NamedNode list)  = Yzl.map(value, "configMap")
  static member Default = Inventory()
  static member yzl (build:NamedNode list) : Node = build |> lift
type Image() =
  static member newTag (value: string)  = Yzl.str(value, "newTag")
  static member newTag (value: Str)  = Yzl.str(value, "newTag")
  static member newName (value: string)  = Yzl.str(value, "newName")
  static member newName (value: Str)  = Yzl.str(value, "newName")
  static member digest (value: string)  = Yzl.str(value, "digest")
  static member digest (value: Str)  = Yzl.str(value, "digest")
  static member Default = Image()
  static member yzl (build:NamedNode list) : Node = build |> lift
type HelmChart() =
  static member includeCRDs (value: bool)  = Yzl.boolean(value, "includeCRDs")
  static member valuesMerge (value: string)  = Yzl.str(value, "valuesMerge")
  static member valuesMerge (value: Str)  = Yzl.str(value, "valuesMerge")
  static member valuesInline (value: NamedNode list)  = Yzl.map(value, "valuesInline")
  static member valuesFile (value: string)  = Yzl.str(value, "valuesFile")
  static member valuesFile (value: Str)  = Yzl.str(value, "valuesFile")
  static member releaseName (value: string)  = Yzl.str(value, "releaseName")
  static member releaseName (value: Str)  = Yzl.str(value, "releaseName")
  static member repo (value: string)  = Yzl.str(value, "repo")
  static member repo (value: Str)  = Yzl.str(value, "repo")
  static member Default = HelmChart()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// GeneratorOptions modify behavior of all ConfigMap and Secret generators
type GeneratorOptions() =
  /// Immutable if true add to all generated resources
  static member immutable (value: bool)  = Yzl.boolean(value, "immutable")
  /// DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents
  static member disableNameSuffixHash (value: bool)  = Yzl.boolean(value, "disableNameSuffixHash")
  static member Default = GeneratorOptions()
  static member yzl (build:NamedNode list) : Node = build |> lift
type FieldSpec() =
  static member create (value: bool)  = Yzl.boolean(value, "create")
  static member Default = FieldSpec()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// Contains the fieldPath to an object field
type FieldSelector() =
  static member fieldpath (value: string)  = Yzl.str(value, "fieldpath")
  static member fieldpath (value: Str)  = Yzl.str(value, "fieldpath")
  static member Default = FieldSelector()
  static member yzl (build:NamedNode list) : Node = build |> lift
/// ConfigMapArgs contains the metadata of how to generate a configmap
type ConfigMapArgs() =
  static member Default = ConfigMapArgs()
  static member yzl (build:NamedNode list) : Node = build |> lift

