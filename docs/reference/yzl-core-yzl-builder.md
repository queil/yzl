## Yzl.Builder Type

Namespace: [Yzl.Core](http://localhost:8089/reference/yzl-core)

Assembly: Yzl.dll

Parent Module: [Yzl](http://localhost:8089/reference/yzl-core-yzl)

Base Type: <code>obj</code>



### Constructors

Constructor | Description | Source
:--- | :--- | :---:
[<code><span>Builder<span>()</span></span></code>](#(+.ctor+)) | Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-builder">Builder</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L76-76)


### Static members

Static member | Description | Source
:--- | :--- | :---:
[<code><span>Builder.boolean&#32;<span>value</span></span></code>](#boolean) | Creates a named boolean scalar node<br /><br />Parameters<br /><br />**value**: <code>bool</code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L87-87)
[<code><span>Builder.float&#32;<span>value</span></span></code>](#float) | Creates a named float scalar node<br /><br />Parameters<br /><br />**value**: <code>float</code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L85-85)
[<code><span>Builder.int&#32;<span>value</span></span></code>](#int) | Creates a named integer scalar node<br /><br />Parameters<br /><br />**value**: <code>int</code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L83-83)
[<code><span>Builder.map&#32;<span>value</span></span></code>](#map) | Creates a named map node<br /><br />Parameters<br /><br />**value**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a>&#32;list</span></code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L89-89)
[<code><span>Builder.named&#32;<span>name&#32;node</span></span></code>](#named) | Parameters<br /><br />**name**: <code>string</code><br />**node**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a></code><br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L77-77)
[<code><span>Builder.none&#32;</span></code>](#none) | Creates an empty node<br /> <br /> *Typically used when generating YAML tree conditionally to indicate no node should be generated*<br /><br />Returns: <code><a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L95-95)
[<code><span>Builder.seq&#32;<span>seq</span></span></code>](#seq) | Creates a named sequence node<br /><br />Parameters<br /><br />**seq**: <code><span><a href="http://localhost:8089/reference/yzl-core-yzl-node">Node</a>&#32;list</span></code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L91-91)
[<code><span>Builder.str&#32;<span>value</span></span></code>](#str) | Creates a named string scalar node from Str<br /><br />Parameters<br /><br />**value**: <code><a href="http://localhost:8089/reference/yzl-core-yzl-str">Str</a></code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L81-81)
[<code><span>Builder.str&#32;<span>value</span></span></code>](#str) | Creates a named string scalar node from F# string<br /><br />Parameters<br /><br />**value**: <code>string</code><br /><br />Returns: <code><span>string&#32;->&#32;<a href="http://localhost:8089/reference/yzl-core-yzl-namednode">NamedNode</a></span></code><br /> | [![Link to source code](http://localhost:8089/content/img/github.png)](https://github.com/queil/yzl/tree/master/src/Yzl/Yzl.fs#L79-79)



