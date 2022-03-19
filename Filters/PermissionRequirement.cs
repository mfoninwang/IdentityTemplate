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
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }
    }


    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) { return Task.CompletedTask; }

            if (context.User.Identity?.Name == "superadmin@gmail.com") 
                context.Succeed(requirement);

            //var permissionsClaim = context.User.Claims.SingleOrDefault(x => x.Type == "Permission" &&
            //    x.Value == requirement.Permission &&
            //    x.Issuer == "LOCAL AUTHORITY");
            //if (permissionsClaim != null) context.Succeed(requirement);

            bool hasPermission = context.User.HasClaim(x => x.Type == "Permission" &&
                x.Value == requirement.Permission &&
                x.Issuer == "LOCAL AUTHORITY");

            if (hasPermission) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
