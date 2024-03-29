﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using PermissionBasedTemplate.Data;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Services
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

            List<Claim> userClaims = new()
            {
                new Claim(ClaimTypes.GivenName, user.FirstName??String.Empty),
                new Claim(ClaimTypes.Surname, user.LastName??String.Empty),
                new Claim("TenantId", "Tenant 1")       
            };

            foreach (var permission in GetUserPermissions(identity))
            {
                userClaims.Add(new Claim("Permission", permission));
            }

            identity.AddClaims(userClaims);

            return identity;
        }

        private IEnumerable<string> GetUserPermissions(ClaimsIdentity identity)
        {
            var db = _httpContext.HttpContext?.RequestServices.GetService<ApplicationIdentityDbContext>();

            var usersRoles = identity.Claims   
                .Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
                .ToList();

            List<RolePermission> rolePermissions = db.RolePermissions.ToList();

            IEnumerable<string> permissions = (from p in db.RolePermissions
                                               where usersRoles.Contains(p.Role.Name)

                                               select p.Permission.Code).Distinct().ToList();
            return permissions;
        }
    }
}
