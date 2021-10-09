## GeneratorOptions Type

Namespace: [Yzl.Bindings.Kustomize](http://localhost:8089/reference/yzl-bindings-kustomize)

Assembly: Yzl.Bindings.Kustomize.dll

Base Type: <code>obj</code>

GeneratorOptions modify behavior of all ConfigMap and Secret generators

### Constructors

Constructor | Description | Source
:--- | :--- | :---:
[<code><span>GeneratorOptions<span>()</span></span></code>](#(+.ctor+)) | Returns: <code><a href="http://localhost:8089/reference/yzl-bindings-kustomize-generatoroptions">GeneratorOptions</a></code><br /> | &#32;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[<code><span>GeneratorOptions.Default</span></code>](#Default) | Returns: <code><a href="http://localhost:8089/reference/yzl-bindings-kustomize-generatoroptions">GeneratorOptions</a></code><br /> | &#32;
[<code><span>GeneratorOptions.disableNameSuffixHash&#32;<span>value</span></span></code>](#disableNameSuffixHash) | DisableNameSuffixHash if true disables the default behavior of adding a suffix to the names of generated resources that is a hash of the resource contents<br /><br />Parameters<br /><br />**value**: <code>bool</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | &#32;
[<code><span>GeneratorOptions.immutable&#32;<span>value</span></span></code>](#immutable) | Immutable if true add to all generated resources<br /><br />Parameters<br /><br />**value**: <code>bool</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | &#32;
[<code><span>GeneratorOptions.yzl&#32;<span>build</span></span></code>](#yzl) | Parameters<br /><br />**build**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | &#32;



