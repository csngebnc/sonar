using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace IdentityProvider.Api.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService interactionService;
        private readonly IConfiguration configuration;
        public string LogoutId { get; set; } = "";
        public LogoutModel(
            IIdentityServerInteractionService interactionService,
            IConfiguration configuration)
        {
            this.interactionService = interactionService;
            this.configuration = configuration;
        }

        public void OnGet(string logoutId)
        {
            LogoutId = logoutId;
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Redirect(configuration.GetValue<string>("AfterAbortedLogoutUri"));
        }
    }
}
