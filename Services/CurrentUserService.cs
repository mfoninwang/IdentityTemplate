using PermissionBasedTemplate.Interfaces;
using System.Security.Claims;

namespace PermissionBasedTemplate.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            UserId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string? UserId { get; }
    }
}
