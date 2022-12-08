using CSONGE.Domain.Entities;
using IdentityProvider.Domain;
using System;

namespace Shipping.Domain.Entities
{
    public class ShippingUser : IEntity<Guid>
    {
        public string Username { get; set; }
        public UserType Type { get; set; }
    }
}
