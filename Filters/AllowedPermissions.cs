using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;

namespace WebApplication1.Filters
{
    public class AllowedPermissions : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        public AllowedPermissions(ApplicationDbContext context)
        {
            _context = context;
        }

        public AllowedPermissions(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async  Task<IEnumerable<string>> PermissionsForUser(IEnumerable<Claim> claims)
        {
            var usersRoles = claims
                .Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
                .ToList();


            ////This gets all the permissions, with a distinct to remove duplicates
            //var permissionsForUser = await _context.RolePermissions
            //    .Where(x => usersRoles.Contains(x.RoleId))
            //    .SelectMany(x => x.Permission.Code)
            //    .Distinct()
            //    .ToListAsync();

            IEnumerable<string> permissions = await (from p in _context.RolePermissions
                                               where usersRoles.Contains(p.RoleId)
                                               select p.Permission.Code).Distinct().ToListAsync();

            return permissions;
        }

        #region IDisposable Members 
        public void Dispose()
        {
            Dispose();
        }
        #endregion
    }
}
