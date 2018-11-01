using Autofac;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EDoc2.FAQ.EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        const string BROKER_NAME = "edoc2_faq_event_bus";
        const string AUTOFAC_SCOPE_NAME = "edoc2_faq_event_bus";

        private readonly IRabbitMQConnection _rabbitMQConnection;
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly ILifetimeScope _autofac;
        private readonly int _retryCount;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(
            IRabbitMQConnection rabbitMQConnection,
            IEventBusSubscriptionsManager subscriptionsManager,
            ILogger<EventBusRabbitMQ> logger,
            ILifetimeScope autofac,
            string queueName = null,
            int retryCount = 5
            )
        {
            _rabbitMQConnection = rabbitMQConnection ?? throw new ArgumentNullException(nameof(rabbitMQConnection));
            _subscriptionsManager = subscriptionsManager ?? new InMemoryEventBusSubscriptionsManager();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _autofac = autofac;
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _retryCount = retryCount;

            _subscriptionsManager.OnEventRemoved += _subscriptionsManager_OnEventRemoved;
        }

        private void _subscriptionsManager_OnEventRemoved(object sender, string e)
        {
            if (!_rabbitMQConnection.IsConnected)
                _rabbitMQConnection.TryConnect();

            using (var channel = _rabbitMQConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName, exchange: BROKER_NAME, routingKey: e, arguments: null);

                if (_subscriptionsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
            _subscriptionsManager.Clear();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_rabbitMQConnection.IsConnected)
                _rabbitMQConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, provider => TimeSpan.FromSeconds(1 << _retryCount), (e, span) => _logger.LogWarning(e.ToString()));

            using (var channel = _rabbitMQConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var msg = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(msg);

                policy.Execute(() =>
                {
                    var props = channel.CreateBasicProperties();
                    props.DeliveryMode = 2;
                    channel.BasicPublish(exchange: BROKER_NAME, routingKey: eventName, basicProperties: props, body: body);
                });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subscriptionsManager.GetEventKey<T>();
            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName)) return;

            if (!_rabbitMQConnection.IsConnected)
                _rabbitMQConnection.TryConnect();

            using (var channel = _rabbitMQConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);
            }
            _subscriptionsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _subscriptionsManager.RemoveSubscription<T, TH>();
        }

        #region privates

        private IModel CreateConsumerChannel()
        {
            if (!_rabbitMQConnection.IsConnected)
                _rabbitMQConnection.TryConnect();

            var channel = _rabbitMQConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, e) => 
            {
                var eventName = e.RoutingKey;
                var message = Encoding.UTF8.GetString(e.Body);
                await ProcessEvent(eventName, message);

                (sender as EventingBasicConsumer).Model.BasicAck(e.DeliveryTag, false);
            };
            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            channel.CallbackException += (sender, e)=> 
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };
            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (!_subscriptionsManager.HasSubscriptionsForEvent(eventName)) return;

            using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
            {
                var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);

                foreach (var subscription in subscriptions)
                {
                    var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                    var handler = scope.ResolveOptional(subscription.HandlerType);
                    var concreateType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreateType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }

        #endregion


    }
}
