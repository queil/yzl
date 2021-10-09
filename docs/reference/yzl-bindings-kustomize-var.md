## Var Type

Namespace: [Yzl.Bindings.Kustomize](https://queil.github.io/yzl/reference/yzl-bindings-kustomize)

Assembly: Yzl.Bindings.Kustomize.dll

Base Type: <code>obj</code>

Represents a variable whose value will be sourced from a field in a Kubernetes object.

### Constructors

Constructor | Description | Source
:--- | :--- | :---:
[<code><span>Var<span>()</span></span></code>](#(+.ctor+)) | Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-bindings-kustomize-var">Var</a></code><br /> | &#32;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[<code><span>Var.Default</span></code>](#Default) | Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-bindings-kustomize-var">Var</a></code><br /> | &#32;
[<code><span>Var.fieldref&#32;<span>value</span></span></code>](#fieldref) | Refers to the field of the object referred to by objref whose value will be extracted for use in replacing $(FOO)<br /><br />Parameters<br /><br />**value**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | &#32;
[<code><span>Var.objref&#32;<span>value</span></span></code>](#objref) | Refers to a Kubernetes resource under the purview of this kustomization<br /><br />Parameters<br /><br />**value**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | &#32;
[<code><span>Var.yzl&#32;<span>build</span></span></code>](#yzl) | Parameters<br /><br />**build**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br /> | &#32;



