## Yzl.Node Type

Namespace: [Yzl.Core](http://localhost:8089/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](http://localhost:8089/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: <code><span><a href="https://docs.microsoft.com/dotnet/api/system.iequatable-1">IEquatable</a>&lt;<a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

YAML node types

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[<code><span>MapNode&#32;<span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></span></code>](#MapNode) | YAML mapping<br /><br />Parameters<br /><br />**Item**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L46-46)
[<code><span>NoNode</span></code>](#NoNode) | Node with no representation<br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L52-52)
[<code><span>Scalar&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-scalar">Scalar</a></span></code>](#Scalar) | YAML scalar<br /><br />Parameters<br /><br />**Item**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-scalar">Scalar</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L50-50)
[<code><span>SeqNode&#32;<span><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&#32;list</span></span></code>](#SeqNode) | YAML sequence<br /><br />Parameters<br /><br />**Item**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L48-48)


### Static members

Static member | Description | Source
:--- | :--- | :---:
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L66-66)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L65-65)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span>string&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L64-64)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span>bool&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L63-63)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span>double&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L62-62)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span>int&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L61-61)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span><span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span>&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L60-60)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L59-59)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L58-58)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L57-57)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code>string</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L56-56)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code>bool</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L55-55)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code>double</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L54-54)
[<code><span>op_Implicit<span>source</span></span></code>](#op_Implicit) | Parameters<br /><br />**source**: <code>int</code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L53-53)



