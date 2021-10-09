## Yzl Module

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll



### Types

Type | Description | Source
:--- | :--- | :---:
[Builder](https://queil.github.io/yzl/reference/yzl-core-yzl-builder) | &#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L76-76)
[Name](https://queil.github.io/yzl/reference/yzl-core-yzl-name) | Represents the key in a YAML key-value pair | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L41-41)
[NamedNode](https://queil.github.io/yzl/reference/yzl-core-yzl-namednode) | YAML key-value pair | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L68-68)
[Node](https://queil.github.io/yzl/reference/yzl-core-yzl-node) | YAML node types | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L44-44)
[RenderOptions](https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions) | YAML rendering options | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L141-141)
[Scalar](https://queil.github.io/yzl/reference/yzl-core-yzl-scalar) | YAML scalar types | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L71-71)
[Str](https://queil.github.io/yzl/reference/yzl-core-yzl-str) | [YAML string types](https://yaml-multiline.info/) | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L17-17)


### Functions and values

Function or value | Description | Source
:--- | :--- | :---:
[<code><span>Yzl.lift&#32;<span>x</span></span></code>](#lift) | Lifts a given object to a <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a><br /><br />*Possible mappings are specified as implicit casts of the <a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a> type*<br /><br />Parameters<br /><br />**x**: <code>^a</code><br /><br />Returns: <code>^b</code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L103-103)
[<code><span>Yzl.liftMany&#32;<span>x</span></span></code>](#liftMany) | Parameters<br /><br />**x**: <code><span>^a&#32;list</span></code><br /><br />Returns: <code><span>^b&#32;list</span></code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L106-106)
[<code><span>Yzl.render&#32;<span>obj</span></span></code>](#render) | Renders Yzl tree into YAML with the default RenderOptions<br /><br />Parameters<br /><br />**obj**: <code>^a</code><br /><br />Returns: <code>string</code><br /><br />Example<br />Render an Yzl node: `! 5 |> Yzl.render`<br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L269-269)
[<code><span>Yzl.renderYaml&#32;<span>opts&#32;yaml</span></span></code>](#renderYaml) | Renders Yzl tree into YAML<br /><br />Parameters<br /><br />**opts**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-renderoptions">RenderOptions</a></code><br />**yaml**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br /><br />Returns: <code>string</code><br /> | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L162-162)



