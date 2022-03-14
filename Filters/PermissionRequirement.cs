using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using WebApplication1.Extenstions;

namespace WebApplication1.Filters
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }


    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) { return; }

            if (context.User.Identity.Name == "superadmin@gmail.com")
            {
                context.Succeed(requirement);
                return; 
            }

            //var permissions = context.User.Claims.Where(x => x.Type == "Permission" &&
            //    x.Value == requirement.Permission &&
            //    x.Issuer == "LOCAL AUTHORITY");

            //if (permissions.Any())
            //{
            //    context.Succeed(requirement);
            //    return;
            //}


            if (context.User.HasPermission(requirement.Permission))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
