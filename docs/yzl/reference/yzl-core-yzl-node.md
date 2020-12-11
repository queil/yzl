# Yzl.Node Type

Namespace: [Yzl.Core](/yzl/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](/yzl/reference/yzl-core-yzl)

Base Type: <code>obj</code>

All Interfaces: , <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralequatable">IStructuralEquatable</a></code>, <code><span><a href="https://docs.microsoft.com/dotnet/api/system.icomparable-1">IComparable</a>&lt;<a href="/yzl/reference/yzl-core-yzl-node">Node</a>&gt;</span></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.icomparable">IComparable</a></code>, <code><a href="https://docs.microsoft.com/dotnet/api/system.collections.istructuralcomparable">IStructuralComparable</a></code>

YAML node types

### Union cases

Union case | Description | Source
:--- | :--- | :---:
[MapNode](#MapNode)&nbsp; | YAML mapping<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code><span><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L46-46)&nbsp;
[NoNode](#NoNode)&nbsp; | Node with no representation<br />&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L52-52)&nbsp;
[Scalar](#Scalar)&nbsp; | YAML scalar<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code><a href="/yzl/reference/yzl-core-yzl-scalar">Scalar</a></code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L50-50)&nbsp;
[SeqNode](#SeqNode)&nbsp; | YAML sequence<br />&nbsp;<br />Parameters: &nbsp;<br />**Item**:<code><span><a href="/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L48-48)&nbsp;


### Static members

Static member | Description | Source
:--- | :--- | :---:
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L60-60)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L59-59)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code><span><a href="/yzl/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L58-58)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code><span><a href="/yzl/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L57-57)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code>string</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L56-56)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code>bool</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L55-55)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code>double</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L54-54)&nbsp;
[op_Implicit](#op_Implicit)&nbsp; | Parameters: &nbsp;<br />**source**:<code>int</code>&nbsp;<br />&nbsp;&nbsp;<br />Returns:<code><a href="/yzl/reference/yzl-core-yzl-node">Node</a></code>&nbsp; | [![Link to source code](/yzl/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl.fs#L53-53)&nbsp;



