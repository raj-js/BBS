using Autofac;
using EDoc2.FAQ.Api.Infrastructure.IntegrationEvents;
using EDoc2.FAQ.Core.Infrastructure.Settings;
using EDoc2.FAQ.EventBus;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public static class EventBusExtensions
    {
        public static void AddEventBus(this IServiceCollection services, EventBusSetting setting)
        {
            services.AddSingleton<IRabbitMQConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQConnection>>();
                var factory = new ConnectionFactory
                {
                    HostName = setting.HostName,
                    UserName = setting.UserName,
                    Password = setting.Password,
                    Port = setting.Port
                };
                return new DefaultRabbitMQConnection(factory, logger, setting.RetryCount);
            });

            services.AddSingleton<IEventBus>(provider =>
            {
                var subscriptionClientName = setting.SubscriptionClientName;
                var rabbitMqConnection = provider.GetRequiredService<IRabbitMQConnection>();
                var iLifetimeScope = provider.GetRequiredService<ILifetimeScope>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMqConnection, eventBusSubscriptionsManager, logger, iLifetimeScope, subscriptionClientName, setting.RetryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<MailSendEventHandler>();
        }

        public static void ConfigureEventBus(this IApplicationBuilder builder)
        {
            var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<MailSendEvent, MailSendEventHandler>();
        }
    }
}
