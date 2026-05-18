using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeWeb.Controllers
{
    public class RepondreController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
