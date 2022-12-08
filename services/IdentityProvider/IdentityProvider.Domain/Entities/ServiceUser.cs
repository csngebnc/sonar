using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityProvider.Domain.Entities
{
    public class ServiceUser : IdentityUser<Guid>
    {
        public UserType Type { get; set; }
    }
}
