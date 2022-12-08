using CSONGE.Messaging.Domain.Messages.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSONGE.Messaging.Extensions
{
    public static class ReceiveEndpointConfiguratorExtensions
    {
        public static void ConfigureEventConsumer<TEvent, TEventConsumer>(this IReceiveEndpointConfigurator configurator,
            IRegistrationContext registration)
            where TEvent : IntegrationEventBase
            where TEventConsumer : class, IConsumer<TEvent>
        {
            configurator.ConfigureConsumer<TEventConsumer>(registration);
        }

        private static void ConfigureEventConsumer(this IReceiveEndpointConfigurator configurator,
            IRegistrationContext registration,
            Type eventType,
            Type consumerType)
        {
            if (!consumerType.IsAssignableTo(typeof(IConsumer<>).MakeGenericType(eventType)))
            {
                throw new InvalidOperationException($"The consumer type of {consumerType.FullName} can not consume events of type {eventType.FullName}.");
            }

            configurator.ConfigureConsumer(registration, consumerType);
        }

        public static void ConfigureEventConsumers(this IReceiveEndpointConfigurator configurator,
            IRegistrationContext registration,
            Assembly assembly,
            params Assembly[] assemblies)
        {
            configurator.ConfigureEventConsumers(registration, assemblies.Append(assembly));
        }

        internal static void ConfigureEventConsumers(this IReceiveEndpointConfigurator configurator,
            IRegistrationContext registration,
            IEnumerable<Assembly> assemblies)
        {
            var eventConsumerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces()
                    .Any(x => x.IsConsumerOfSubType<IntegrationEventBase>()))
                .Select(x => new
                {
                    ConsumerType = x,
                    EventTypes = x.GetInterfaces()
                        .Where(i => i.IsConsumerOfSubType<IntegrationEventBase>())
                        .Select(i => i.GetGenericArguments().Single())
                });

            foreach (var consumerType in eventConsumerTypes)
            {
                foreach (var eventType in consumerType.EventTypes)
                {
                    configurator.ConfigureEventConsumer(registration, eventType, consumerType.ConsumerType);
                }
            }
        }
    }
}
