using Microsoft.AspNetCore.Mvc;

namespace OdooScopeWeb.Controllers
{
    public class MainMenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
