using Autofac;
using EDoc2.FAQ.Api.Infrastructure.IntegrationEvents;
using EDoc2.FAQ.EventBus;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public static class EventBusExtensions
    {
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusConfig = configuration.GetSection("EventBus");
            var retryCount = eventBusConfig.GetValue<int>("RetryCount");
            var hostName = eventBusConfig.GetValue<string>("HostName");
            var userName = eventBusConfig.GetValue<string>("UserName");
            var password = eventBusConfig.GetValue<string>("Password");
            var port = eventBusConfig.GetValue<int>("Port");

            services.AddSingleton<IRabbitMQConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQConnection>>();
                var factory = new ConnectionFactory
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                    Port = port
                };
                return new DefaultRabbitMQConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus>(provider =>
            {
                var subscriptionClientName = eventBusConfig["SubscriptionClientName"];
                var rabbitMQConnection = provider.GetRequiredService<IRabbitMQConnection>();
                var iLifetimeScope = provider.GetRequiredService<ILifetimeScope>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQConnection, eventBusSubscriptionsManager, logger, iLifetimeScope, subscriptionClientName, retryCount);
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
