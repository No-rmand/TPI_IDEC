using Microsoft.AspNetCore.Mvc;

namespace OdooScopeWeb.Controllers
{
    public class MainMenuController : Controller
    {

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
//VU//