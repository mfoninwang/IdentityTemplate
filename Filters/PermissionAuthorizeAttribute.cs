using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Extenstions;

namespace WebApplication1.Filters
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
            string userName = user.Identity.Name;

            if (userName == "superadmin@gmail.com") return;

            var db = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            var permission = db.Permissions
                .Include(r=>r.Roles)
                .Single(n => n.Code == this.Permission);
            
            if (permission.Roles != null)
            {
                foreach (var item in permission.Roles)
                {
                    if (user.IsInRole(item.RoleId)) return;
                }
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
