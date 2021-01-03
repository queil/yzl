# Yzl.Str Type

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](https://queil.github.io/yzl/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: <code><span><a href="https://docs.microsoft.com/dotnet/api/system.iequatable-1">IEquatable</a>&lt;<a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

[YAML string types](https://yaml-multiline.info/)

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[DoubleQuoted](#DoubleQuoted)&#32; | YAML double-quoted string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L23-23)&#32;
[Folded](#Folded)&#32; | YAML > string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L25-25)&#32;
[FoldedKeep](#FoldedKeep)&#32; | YAML >+ string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L29-29)&#32;
[FoldedStrip](#FoldedStrip)&#32; | YAML >- string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L27-27)&#32;
[Literal](#Literal)&#32; | YAML &#124; string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L31-31)&#32;
[LiteralKeep](#LiteralKeep)&#32; | YAML &#124;+ string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L35-35)&#32;
[LiteralStrip](#LiteralStrip)&#32; | YAML &#124;- string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L33-33)&#32;
[Plain](#Plain)&#32; | YAML plain string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L19-19)&#32;
[SingleQuoted](#SingleQuoted)&#32; | YAML single-quoted string<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code>string</code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L21-21)&#32;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L38-38)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code>string</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-str">Str</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L37-37)&#32;



