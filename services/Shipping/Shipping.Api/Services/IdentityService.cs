using IdentityModel;
using Microsoft.AspNetCore.Http;
using Shipping.Application.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace Shipping.Api.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpContext httpContext;

        public IdentityService(
            IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public Guid GetCurrentUserId()
        {
            var userId = httpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject).Value;
            return Guid.Parse(userId);
        }
    }
}
