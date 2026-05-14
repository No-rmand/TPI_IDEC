using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;

namespace OdooScopeWeb.Controllers
{
    public class SecteurActiviteController : Controller
    {
        private SqlServerContext _context;
        public SecteurActiviteController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult New()
        {
            return View();
        }

        public IActionResult List()
        {
            List<SecteurActivite> liste = _context.SecteurActivites.ToList();
            return View(liste);
        }
    }
}
