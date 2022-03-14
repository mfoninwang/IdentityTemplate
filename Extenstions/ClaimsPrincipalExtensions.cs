using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Entities;

namespace WebApplication1.Extenstions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetTenantId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("Tenant 1");
        }

        public static bool HasPermission(this ClaimsPrincipal principal, string permission)
        {
            return false;
        }

        public static IEnumerable<string> Permissions(this ClaimsPrincipal principal)
        {
            string[] permissions = new string[]
            {
                "PERMISSION.ROLE.LIST"
            };
            return permissions.ToList();
        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal, string id)
        {
            var currentUserId = GetUserId(principal);

            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
