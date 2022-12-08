using CSONGE.Messaging.Configuration;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CSONGE.Messaging.Extensions.Rabbitmq
{
    public static class MessagingConfiguratorExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection configurator,
            IConfigurationSection rabbitMqConfigSection,
            IConfigurationSection messagingConfigSection,
            Assembly[] sentCommandsAssemblies = null,
            Assembly[] consumerAssemblies = null)
        {
            var rabbitMqOptions = new RabbitMQConfiguration();
            rabbitMqConfigSection.Bind(rabbitMqOptions);
            var messagingOptions = new MessagingConfiguration();
            messagingConfigSection.Bind(messagingOptions);

            if (sentCommandsAssemblies != null)
            {
                MessagingConventions.RegisterCommands(sentCommandsAssemblies);
            }

            configurator.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                        host: rabbitMqOptions.HostName,
                        port: rabbitMqOptions.Port,
                        virtualHost: rabbitMqOptions.VirtualHost,
                        configure: config =>
                        {
                            config.Username(rabbitMqOptions.Username);
                            config.Password(rabbitMqOptions.Password);
                        });

                    cfg.UseMessageRetry(c =>
                    {
                        c.Exponential(
                            retryLimit: messagingOptions.ConsumerErrorHandling.RetryCount,
                            minInterval: messagingOptions.ConsumerErrorHandling.MinInterval,
                            maxInterval: messagingOptions.ConsumerErrorHandling.MaxInterval,
                            intervalDelta: messagingOptions.ConsumerErrorHandling.ExponentialBaseIntervalDelta);
                    });

                    if (consumerAssemblies != null)
                    {
                        // separate receive endpoints for every command
                        cfg.ConfigureCommandConsumerEndpoints(context, consumerAssemblies);

                        // single receive endpoint shared by all of the events
                        cfg.ReceiveEndpoint(rabbitMqOptions.QueueName, e =>
                        {
                            e.ConfigureEventConsumers(context, consumerAssemblies);
                        });
                    }
                });

                if (consumerAssemblies != null)
                {
                    configurator.AddConsumers(consumerAssemblies);
                }
            });

            return configurator;
        }
    }
}
