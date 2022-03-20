using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PermissionBasedTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
