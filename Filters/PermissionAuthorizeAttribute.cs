using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionBasedTemplate.Extenstions;

namespace PermissionBasedTemplate.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Permission { get; }
        public PermissionAttribute(string permission) : base("Permission")
        {
            Permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.Name == "superadmin@gmail.com") return;

            bool hasPermission = user.HasClaim(x => x.Type == "Permission" &&
               x.Value == this.Permission && x.Issuer == "LOCAL AUTHORITY");

            if (hasPermission) return;

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
