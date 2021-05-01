## Yzl Module

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll



### Types

Type | Description | Source
:--- | :--- | :---:
[Name](https://queil.github.io/yzl/reference/yzl-core-yzl-name) | Represents the key in a YAML key-value pair | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L41-41)
[NamedNode](https://queil.github.io/yzl/reference/yzl-core-yzl-namednode) | YAML key-value pair | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L67-67)
[Node](https://queil.github.io/yzl/reference/yzl-core-yzl-node) | YAML node types | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L44-44)
[RenderOptions](https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions) | YAML rendering options | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L110-110)
[Scalar](https://queil.github.io/yzl/reference/yzl-core-yzl-scalar) | YAML scalar types | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L70-70)
[Str](https://queil.github.io/yzl/reference/yzl-core-yzl-str) | [YAML string types](https://yaml-multiline.info/) | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L17-17)


### Functions and values

Function or value | Description | Source
:--- | :--- | :---:
[<code><span>Yzl.augment&#32;<span>x</span></span></code>](#augment) | Augments a given object to a <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a><br /><br />*Possible augmentations are specified as implicit casts of the <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a> type*<br /><br />Parameters<br /><br />**x**: <code>^a</code><br /><br />Returns: <code>^b</code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L82-82)
[<code><span>Yzl.boolean&#32;<span>name&#32;value</span></span></code>](#boolean) | Creates a named boolean scalar node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**value**: <code>bool</code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L96-96)
[<code><span>Yzl.float&#32;<span>name&#32;value</span></span></code>](#float) | Creates a named float scalar node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**value**: <code>double</code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L93-93)
[<code><span>Yzl.int&#32;<span>name&#32;value</span></span></code>](#int) | Creates a named integer scalar node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**value**: <code>int</code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L90-90)
[<code><span>Yzl.map&#32;<span>name&#32;map</span></span></code>](#map) | Creates a named map node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**map**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L99-99)
[<code><span>Yzl.none&#32;<span></span></span></code>](#none) | Creates an empty node<br /> <br /> *Typically used when generating YAML tree conditionally to indicate no node should be generated*<br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L107-107)
[<code><span>Yzl.render&#32;<span>yaml</span></span></code>](#render) | Renders Yzl tree into YAML with the default RenderOptions<br /><br />Parameters<br /><br />**yaml**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br /><br />Returns: <code>string</code><br /><br />Example<br />Render an Yzl node: `! 5 |> Yzl.render `<br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L236-236)
[<code><span>Yzl.renderYaml&#32;<span>opts&#32;yaml</span></span></code>](#renderYaml) | Renders Yzl tree into YAML<br /><br />Parameters<br /><br />**opts**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions">RenderOptions</a></code><br />**yaml**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br /><br />Returns: <code>string</code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L131-131)
[<code><span>Yzl.seq&#32;<span>name&#32;seq</span></span></code>](#seq) | Creates a named sequence node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**seq**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L102-102)
[<code><span>Yzl.str&#32;<span>name&#32;node</span></span></code>](#str) | Creates a named string scalar node<br /><br />Parameters<br /><br />**name**: <code>string</code><br />**node**: <code>^a</code><br /><br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L87-87)



