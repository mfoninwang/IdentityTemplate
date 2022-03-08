using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Areas.Admin.Controllers
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
