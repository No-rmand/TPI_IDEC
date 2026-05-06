using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;

namespace OdooScopeWeb.Controllers
{
    public class ClientListController : Controller
    {

        private SqlServerContext _context;
        public ClientListController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult DisplayClientList()
        {
            List<Client> liste = _context.Clients.ToList();
            return View(liste);
        }
    }
}
