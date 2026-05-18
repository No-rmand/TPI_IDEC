using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeWeb.Controllers
{
    public class ResultatController : Controller
    {
        private SqlServerContext _context;
        public ResultatController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult Result()
        {
            List<ApplicationOdoo> list = _context.ApplicationOdoos.ToList();
            return View(list);
        }
    }
}
