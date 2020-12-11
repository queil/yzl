# Yzl.Str Type

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](https://queil.github.io/yzl/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: , <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

[YAML string types](https://yaml-multiline.info/)

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[DoubleQuoted](#DoubleQuoted)&nbsp; | YAML double-quoted string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L23-23)&nbsp;
[Folded](#Folded)&nbsp; | YAML > string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L25-25)&nbsp;
[FoldedKeep](#FoldedKeep)&nbsp; | YAML >+ string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L29-29)&nbsp;
[FoldedStrip](#FoldedStrip)&nbsp; | YAML >- string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L27-27)&nbsp;
[Literal](#Literal)&nbsp; | YAML &#124; string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L31-31)&nbsp;
[LiteralKeep](#LiteralKeep)&nbsp; | YAML &#124;+ string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L35-35)&nbsp;
[LiteralStrip](#LiteralStrip)&nbsp; | YAML &#124;- string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L33-33)&nbsp;
[Plain](#Plain)&nbsp; | YAML plain string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L19-19)&nbsp;
[SingleQuoted](#SingleQuoted)&nbsp; | YAML single-quoted string<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code>string</code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L21-21)&nbsp;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code>&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L38-38)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code>string</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code>&nbsp; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L37-37)&nbsp;



