# Yzl.Node Type

Namespace: [Yzl.Core](https://queil.github.io/yzl/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](https://queil.github.io/yzl/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: <code><span><a href="https://docs.microsoft.com/dotnet/api/system.iequatable-1">IEquatable</a>&lt;<a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

YAML node types

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[MapNode](#MapNode)&#32; | YAML mapping<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L46-46)&#32;
[NoNode](#NoNode)&#32; | Node with no representation<br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L52-52)&#32;
[Scalar](#Scalar)&#32; | YAML scalar<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-scalar">Scalar</a></code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L50-50)&#32;
[SeqNode](#SeqNode)&#32; | YAML sequence<br />&#32;<br />Parameters&#32;<br />&#32;&#32;<br />**Item**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&#32;<br />&#32;&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L48-48)&#32;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L64-64)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L63-63)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span>string&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L62-62)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span>bool&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L61-61)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span>double&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L60-60)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span>int&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L59-59)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L58-58)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code><span><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L57-57)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code>string</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L56-56)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code>bool</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L55-55)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code>double</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L54-54)&#32;
[op_Implicit](#op_Implicit)&#32; | Parameters&#32;<br />&#32;&#32;<br />**source**: <code>int</code>&#32;<br />&#32;&#32;<br />Returns: <code><a href="https://queil.github.io/yzl/reference/yzl-core-yzl-node">Node</a></code><br />&#32; | [![Link to source code](https://queil.github.io/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L53-53)&#32;



