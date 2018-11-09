using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;

namespace EDoc2.FAQ.Search.Engine
{
    public class ClientBuilder
    {
        private ConnectionSettings _settings;
        private IConnectionPool _connectionPool;

        public ClientBuilder UseSingleNode(Uri node)
        {
            _connectionPool = new SingleNodeConnectionPool(node);
            _settings = new ConnectionSettings(_connectionPool);
            return this;
        }

        public ClientBuilder UseMultipleNodes(IEnumerable<Uri> nodes)
        {
            _connectionPool = new StaticConnectionPool(nodes);
            _settings = new ConnectionSettings(_connectionPool); 
            return this;
        }

        public ClientBuilder Authenticate(string userName, string password)
        {
            if (_settings == null)
                throw new InvalidOperationException();

            _settings.BasicAuthentication(userName, password);
            return this;
        }

        public ClientBuilder UseDebug()
        {
            if (_settings == null)
                throw new InvalidOperationException();

            _settings.EnableDebugMode();
            _settings.PrettyJson();
            _settings.DisableDirectStreaming(true);
            _settings.ThrowExceptions(true);
            return this;
        }

        public ClientBuilder UseNormalSetting(int connectionLimit = 80)
        {
            if (_settings == null)
                throw new InvalidOperationException();

            _settings.ConnectionLimit(connectionLimit);
            _settings.DefaultFieldNameInferrer(s => s);
            _settings.DefaultTypeNameInferrer(s => s.Name);
            return this;
        }

        public ElasticClient Build()
        {
            if (_settings == null)
                throw new InvalidOperationException();

            return new ElasticClient(_settings);
        }
    }
}
