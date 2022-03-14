using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Extenstions
{
    public class ApplicationUserClaimsPrincipalFactory 
        : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly IHttpContextAccessor _httpContext;

        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor
            , IHttpContextAccessor httpContext)
        : base(userManager, roleManager, optionsAccessor)
        {
            _httpContext = httpContext;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] 
                {       
                    new Claim(ClaimTypes.GivenName, user.FirstName)  
                });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] 
                {
                    new Claim(ClaimTypes.Surname, user.LastName),
                });
            }

            return principal;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName??string.Empty));
            identity.AddClaim(new Claim("TenantId", "Tenant1"));

            return identity;
        }
    }
}
