using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OdooScopeEntities.Entities;

namespace OdooScopeWeb.Controllers
{

    
    public class NewClientController : Controller
    {

        private SqlServerContext _context;
        public NewClientController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult NewClientForm()
        {
            return View();
        }
    }
}
