using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebShopApp.DAL.Models;
using WebShopApp.Infrastructure.Interface;

namespace WebShopApp.Infrastructure.Authorization
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        private IRepository repository;

        public AppClaimsPrincipalFactory(
            IRepository repository,
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            this.repository = repository;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("FirstName", user.FirstName ?? ""));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("LastName", user.LastName ?? ""));

            return principal;
        }

    }
}
