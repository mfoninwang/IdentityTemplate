using System.Security.Claims;

namespace PermissionBasedTemplate.Extenstions
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
            return principal.Claims.Any(x => x.Type == "Permission" && x.Value == permission);
        }

        public static IEnumerable<string> Permissions(this ClaimsPrincipal principal)
        {
            var permissions = (from p in principal.Claims 
                               where p.Type =="Permission"                              
                               select p.Value).ToList();

            return permissions;
        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal, string id)
        {
            var currentUserId = GetUserId(principal);

            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
