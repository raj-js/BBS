using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace EDoc2.FAQ.Search.Engine
{
    public class Manager
    {
        /// <summary>
        /// es 节点
        /// </summary>
        private readonly List<Uri> _nodes;


        public Manager(List<Uri> nodes)
        {
            _nodes = nodes;

        }


        public IConnectionPool CreateConnectionPool()
        {
            if(_nodes.Count == 1)
                return new SingleNodeConnectionPool(_nodes.First());



            return null;
        }
    }
}
