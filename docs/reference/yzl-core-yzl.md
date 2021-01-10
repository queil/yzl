# Yzl Module

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll



### Types

Type | Description | Source
:--- | :--- | :---:
[Name](https://queil.github.io/yzl/reference/yzl-core-yzl-name)&#32; | Represents the key in a YAML key-value pair&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L41-41)&#32;
[NamedNode](https://queil.github.io/yzl/reference/yzl-core-yzl-namednode)&#32; | YAML key-value pair&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L66-66)&#32;
[Node](https://queil.github.io/yzl/reference/yzl-core-yzl-node)&#32; | YAML node types&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L44-44)&#32;
[RenderOptions](https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions)&#32; | YAML rendering options&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L109-109)&#32;
[Scalar](https://queil.github.io/yzl/reference/yzl-core-yzl-scalar)&#32; | YAML scalar types&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L69-69)&#32;
[Str](https://queil.github.io/yzl/reference/yzl-core-yzl-str)&#32; | [YAML string types](https://yaml-multiline.info/)&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L17-17)&#32;


### Functions and values

Function or value | Description | Source
:--- | :--- | :---:
[augment](#augment)&#32; | Augments a given object to a <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a><br />&#32;<br />*Possible augmentations are specified as implicit casts of the <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a> type*<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**x**: <code>^a</code>&#32;<br />&#32;&#32;<br />Returns: <code>^b</code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L81-81)&#32;
[boolean](#boolean)&#32; | Creates a named boolean scalar node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**value**: <code>bool</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L95-95)&#32;
[float](#float)&#32; | Creates a named float scalar node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**value**: <code>double</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L92-92)&#32;
[int](#int)&#32; | Creates a named integer scalar node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**value**: <code>int</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L89-89)&#32;
[map](#map)&#32; | Creates a named map node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**map**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L98-98)&#32;
[none](#none)&#32; | Creates an empty node<br /> <br /> *Typically used when generating YAML tree conditionally to indicate no node should be generated*<br />&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L106-106)&#32;
[render](#render)&#32; | Renders Yzl tree into YAML with the default RenderOptions<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**yaml**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code>&#32;<br />&#32;&#32;<br />Returns: <code>string</code><br />&#32;<br />Example&#32;<br />Render an Yzl node: `! 5 |> Yzl.render `<br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L230-230)&#32;
[renderYaml](#renderYaml)&#32; | Renders Yzl tree into YAML<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**opts**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions">RenderOptions</a></code>&#32;<br />**yaml**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code>&#32;<br />&#32;&#32;<br />Returns: <code>string</code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L125-125)&#32;
[seq](#seq)&#32; | Creates a named sequence node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**seq**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L101-101)&#32;
[str](#str)&#32; | Creates a named string scalar node<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**name**: <code>string</code>&#32;<br />**node**: <code>^a</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L86-86)&#32;



