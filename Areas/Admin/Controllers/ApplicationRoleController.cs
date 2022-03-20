#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionBasedTemplate.Data;
using PermissionBasedTemplate.Filters;
using PermissionBasedTemplate.Identity;

namespace PermissionBasedTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "PERMISSION.ROLE.LIST")]
    public class ApplicationRoleController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleController(ApplicationIdentityDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: Admin/ApplicationRole
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // GET: Admin/ApplicationRole/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // GET: Admin/ApplicationRole/Create
        [Permission("PERMISSION.ROLE.CREATE")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ApplicationRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PERMISSION.ROLE.CREATE")]
        public async Task<IActionResult> Create([Bind("CreateDate,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(applicationRole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.Roles.FindAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            return View(applicationRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CreateDate,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationRoleExists(applicationRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationRole);
        }

        private bool ApplicationRoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
