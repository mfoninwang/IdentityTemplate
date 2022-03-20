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

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.Name == "superadmin@gmail.com") return;

            if (user.HasPermission(this.Permission)) return;

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
