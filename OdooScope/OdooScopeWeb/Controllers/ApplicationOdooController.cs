using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdooScopeEntities.Entities;

namespace OdooScopeWeb.Controllers
{

    public class ApplicationOdooController : Controller
    {
        private SqlServerContext _context;
        public ApplicationOdooController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            List<ApplicationOdoo> liste = _context.ApplicationOdoos.Include(sa => sa.SecteurActivite).ToList();
            return View(liste);

        }

    }
}
