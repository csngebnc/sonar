using CSONGE.Messaging.Domain.Messages;
using MassTransit;
using System;
using System.Linq;

namespace CSONGE.Messaging.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsConsumerOfSubType<TMessage>(this Type @interface)
            where TMessage : MessageBase
        {
            return @interface.IsGenericType
                && @interface.GetGenericTypeDefinition() == typeof(IConsumer<>)
                && @interface.GetGenericArguments().Single().IsAssignableTo(typeof(TMessage));
        }
    }
}
