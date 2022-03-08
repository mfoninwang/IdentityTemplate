using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Filters
{
    public class PermissionAuthorizeAttribute: AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(params string[] permissions)
        {

        }
    }
}
