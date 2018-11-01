using RabbitMQ.Client;
using System;

namespace EDoc2.FAQ.EventBus.RabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
