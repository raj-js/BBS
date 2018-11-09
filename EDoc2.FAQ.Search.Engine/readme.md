### NEST --high level elasticsearch .net client

#### 连接池

- SingleNodeConnectionPool
- StaticConnectionPool
- SniffingConnectionPool
- StickyConnectionPool
- StickySniffingConnectionPool

##### SingleNodeConnectionPool 单节点连接池

所有连接池中最简单的连接池， 如果在 `ConnectionSettings` 的构造器中未指定连接池，
默认会使用单节点连接池，它会使用单个 Uri 连接 Elasticsearch 来完成所有的调用。 
单节点连接池没有嗅探以及ping行为，也不会标记节点存活和死亡。
如果你的Elasticsearch集群中只包含一个节点，或者使用单个负载均衡器实例来完成群集的
交互，也比较适合使用 `SingileNodeConnection`


##### StaticConnectionPool 静态连接池

如果你拥有一个已知的小规模集群，并且不想开启嗅探来寻找集群拓扑，`StaticConnectionPool` 
是不错的选择

##### SniffingConnectionPool 嗅探连接池

`SniffingConnectionPool` 派生于 `StaticConnectionPool` , 嗅探连接池允许
在运行的时候进行繁殖

##### StickyConnectionPool 黏性连接池

黏性连接池会返回第一个存活的节点来以处理请求


##### StickySniffingConnectionPool 黏性嗅探连接池

结合 `SniffingConnectionPool` 与 `StickyConnectionPool` 的特性