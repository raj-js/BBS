using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;

namespace EDoc2.FAQ.MQ
{
    public class DefaultRabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<DefaultRabbitMQConnection> _logger;
        private IConnection _connection;
        private bool _disposed;
        private int _retryCount;
        private readonly object mutex = new object();

        public DefaultRabbitMQConnection(
            IConnectionFactory connectionFactory,
            ILogger<DefaultRabbitMQConnection> logger,
            int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected)
                throw new InvalidOperationException();

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            try
            {
                _connection.Close();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
            }
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect...");

            lock (mutex)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, provider => TimeSpan.FromSeconds(1 << _retryCount), (e, span) => _logger.LogWarning(e.ToString()));

                policy.Execute(()=> 
                {
                    _connection = _connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    _connection.CallbackException += OnCallbackException;

                    _logger.LogInformation("RabbitMQ Client has been connected");
                    return true;
                }

                _logger.LogCritical("RabbitMQ connections could not be created and opened");
                return false;
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
    }
}
