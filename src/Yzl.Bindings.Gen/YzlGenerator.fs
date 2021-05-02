namespace Yzl.Bindings.Gen

open NJsonSchema.CodeGeneration

module Generator =

  type YzlGeneratorSettings() =
    inherit CodeGeneratorSettingsBase()

  type YzlTypeResolver(settings:YzlGeneratorSettings) = 
    inherit TypeResolverBase(settings)

      override this.GetOrGenerateTypeName(schema: NJsonSchema.JsonSchema, typeNameHint: string): string = 
          failwith "Not Implemented"
      override this.IsDefinitionTypeSchema(schema: NJsonSchema.JsonSchema): bool = 
          failwith "Not Implemented"
      override this.Resolve(schema: NJsonSchema.JsonSchema, isNullable: bool, typeNameHint: string): string = 
          failwith "Not Implemented"

  type YzlGenerator(rootObject:obj, resolver:YzlTypeResolver, settings:YzlGeneratorSettings) =
    inherit GeneratorBase(rootObject, resolver, settings)
      override this.GenerateFile(artifacts: System.Collections.Generic.IEnumerable<CodeArtifact>): string = 
          failwith "Not Implemented"
      override this.GenerateType(schema: NJsonSchema.JsonSchema, typeNameHint: string): CodeArtifact = 
          failwith "Not Implemented"
      override this.GenerateTypes(): System.Collections.Generic.IEnumerable<CodeArtifact> = 
          failwith "Not Implemented"
