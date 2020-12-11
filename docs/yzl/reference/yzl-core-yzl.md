# Yzl Module

Namespace: [Yzl.Core](/yzl/reference/yzl-core)

Assembly: Yzl.dll



### Types

Type | Description | Source
:--- | :--- | :---:
[Name](/yzl/reference/yzl-core-yzl-name)&nbsp; | Represents the key in a YAML key-value pair&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L41-41)&nbsp;
[NamedNode](/yzl/reference/yzl-core-yzl-namednode)&nbsp; | YAML key-value pair&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L62-62)&nbsp;
[Node](/yzl/reference/yzl-core-yzl-node)&nbsp; | YAML node types&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L44-44)&nbsp;
[RenderOptions](/yzl/reference/yzl-core-yzl-renderoptions)&nbsp; | YAML rendering options&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L105-105)&nbsp;
[Scalar](/yzl/reference/yzl-core-yzl-scalar)&nbsp; | YAML scalar types&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L65-65)&nbsp;
[Str](/yzl/reference/yzl-core-yzl-str)&nbsp; | [YAML string types](https://yaml-multiline.info/)&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L17-17)&nbsp;


### Functions and values

Function or value | Description | Source
:--- | :--- | :---:
[augment](#augment)&nbsp; | Augments a given object to a <a href="/yzl/reference/yzl-core-yzl-node">Node</a><br />&nbsp;<br />*Possible augmentations are specified as implicit casts of the <a href="/yzl/reference/yzl-core-yzl-node">Node</a> type*<br />&nbsp;<br />Parameters: &nbsp;<br />**x**:<code>^a</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code>^b</code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L77-77)&nbsp;
[boolean](#boolean)&nbsp; | Creates a named boolean scalar node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**value**:<code>bool</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L91-91)&nbsp;
[float](#float)&nbsp; | Creates a named float scalar node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**value**:<code>double</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L88-88)&nbsp;
[int](#int)&nbsp; | Creates a named integer scalar node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**value**:<code>int</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L85-85)&nbsp;
[map](#map)&nbsp; | Creates a named map node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**map**:<code><span><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L94-94)&nbsp;
[none](#none)&nbsp; | Creates an empty node<br /> <br /> *Typically used when generating YAML tree conditionally to indicate no node should be generated*<br />&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L102-102)&nbsp;
[render](#render)&nbsp; | Renders Yzl tree into YAML with the default RenderOptions<br />&nbsp;<br />Parameters: &nbsp;<br />**yaml**:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code>string</code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L218-218)&nbsp;
[renderYaml](#renderYaml)&nbsp; | Renders Yzl tree into YAML<br />&nbsp;<br />Parameters: &nbsp;<br />**opts**:<code><a href="/yzl/reference/yzl-core-yzl-renderoptions">RenderOptions</a></code>&nbsp;<br />**yaml**:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code>string</code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L118-118)&nbsp;
[seq](#seq)&nbsp; | Creates a named sequence node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**seq**:<code><span><a href="/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L97-97)&nbsp;
[str](#str)&nbsp; | Creates a named string scalar node<br />&nbsp;<br />Parameters: &nbsp;<br />**name**:<code>string</code>&nbsp;<br />**node**:<code>^a</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L82-82)&nbsp;



