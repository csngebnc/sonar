using CSONGE.Messaging.Domain.Messages.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSONGE.Messaging.Extensions
{
    public static class MessagingConventions
    {
        /// <summary>
        /// Creates a send topology for a command so that it can be sent directly, without specifying an endpoint
        /// </summary>
        /// <typeparam name="TCommand">Type of command</typeparam>
        public static void RegisterCommand<TCommand>()
            where TCommand : IntegrationCommandBase
        {
            EndpointConvention.Map<TCommand>(new Uri($"queue:{typeof(TCommand).Name}"));
        }

        /// <summary>
        /// Creates send topology for all commands that are in the provided assemblies, so that they can be sent directly,
        /// without specifying an endpoint
        /// </summary>
        /// <param name="assemblies">Assemblies to scan for commands</param>
        internal static void RegisterCommands(IEnumerable<Assembly> assemblies)
        {
            var commandTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(x => x.IsAssignableTo(typeof(IntegrationCommandBase)));

            foreach (var commandType in commandTypes)
            {
                var registerCommandMethod = typeof(MessagingConventions).GetMethod(nameof(RegisterCommand))
                    .MakeGenericMethod(commandType);

                registerCommandMethod.Invoke(null, null);
            }
        }
    }
}
