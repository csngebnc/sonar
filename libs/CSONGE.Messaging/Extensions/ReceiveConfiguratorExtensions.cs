using CSONGE.Messaging.Domain.Messages.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSONGE.Messaging.Extensions
{
    public static class ReceiveConfiguratorExtensions
    {
        public static void ConfigureCommandConsumer<TCommand, TCommandConsumer>(this IReceiveConfigurator configurator,
            IRegistrationContext registration)
            where TCommand : IntegrationCommandBase
            where TCommandConsumer : class, IConsumer<TCommand>
        {
            configurator.ReceiveEndpoint(typeof(TCommand).Name, e =>
            {
                e.ConfigureConsumer<TCommandConsumer>(registration);
            });
        }

        private static void ConfigureCommandConsumerEndpoint(this IReceiveConfigurator configurator,
            IRegistrationContext registration,
            Type commandType,
            Type consumerType)
        {
            if (!consumerType.IsAssignableTo(typeof(IConsumer<>).MakeGenericType(commandType)))
            {
                throw new InvalidOperationException($"The consumer type of {consumerType.FullName} can not consume commands of type {commandType.FullName}.");
            }

            configurator.ReceiveEndpoint(commandType.Name, e =>
            {
                e.ConfigureConsumer(registration, consumerType);
            });
        }

        public static void ConfigureCommandConsumerEndpoints(this IReceiveConfigurator configurator,
            IRegistrationContext registration,
            Assembly assembly,
            params Assembly[] assemblies)
        {
            configurator.ConfigureCommandConsumerEndpoints(registration, assemblies.Append(assembly));
        }

        internal static void ConfigureCommandConsumerEndpoints(this IReceiveConfigurator configurator,
            IRegistrationContext registration,
            IEnumerable<Assembly> assemblies)
        {
            var commandConsumerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces()
                    .Any(i => i.IsConsumerOfSubType<IntegrationCommandBase>()))
                .Select(x => new
                {
                    ConsumerType = x,
                    CommandTypes = x.GetInterfaces()
                        .Where(i => i.IsConsumerOfSubType<IntegrationCommandBase>())
                        .Select(i => i.GetGenericArguments().Single())
                });

            foreach (var consumerType in commandConsumerTypes)
            {
                foreach (var commandType in consumerType.CommandTypes)
                {
                    configurator.ConfigureCommandConsumerEndpoint(registration, commandType, consumerType.ConsumerType);
                }
            }
        }
    }
}
