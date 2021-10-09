## Yzl.Str Type

Namespace: [Yzl.Core](http://localhost:8089/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](http://localhost:8089/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: <code><span><a href="https://docs.microsoft.com/dotnet/api/system.iequatable-1">IEquatable</a>&lt;<a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

[YAML string types](https://yaml-multiline.info/)

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[<code><span>DoubleQuoted&#32;string</span></code>](#DoubleQuoted) | YAML double-quoted string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L23-23)
[<code><span>Folded&#32;string</span></code>](#Folded) | YAML > string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L25-25)
[<code><span>FoldedKeep&#32;string</span></code>](#FoldedKeep) | YAML >+ string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L29-29)
[<code><span>FoldedStrip&#32;string</span></code>](#FoldedStrip) | YAML >- string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L27-27)
[<code><span>Literal&#32;string</span></code>](#Literal) | YAML &#124; string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L31-31)
[<code><span>LiteralKeep&#32;string</span></code>](#LiteralKeep) | YAML &#124;+ string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L35-35)
[<code><span>LiteralStrip&#32;string</span></code>](#LiteralStrip) | YAML &#124;- string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L33-33)
[<code><span>Plain&#32;string</span></code>](#Plain) | YAML plain string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L19-19)
[<code><span>SingleQuoted&#32;string</span></code>](#SingleQuoted) | YAML single-quoted string<br /><br />Parameters<br /><br />**Item**: <code>string</code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L21-21)


### Static members

Static member | Description | Source
:--- | :--- | :---:
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L38-38)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code>string</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L37-37)



