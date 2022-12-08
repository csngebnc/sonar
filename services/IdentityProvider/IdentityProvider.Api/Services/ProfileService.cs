using IdentityModel;
using IdentityProvider.Domain.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityProvider.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ServiceUser> userManager;

        public ProfileService(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, user.Type.ToString()));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
