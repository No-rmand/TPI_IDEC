using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OdooScopeEntities.Entities;

namespace OdooScopeWeb.Controllers
{


    public class ClientController : Controller
    {

        private SqlServerContext _context;
        public ClientController(SqlServerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult New()
        {
            ViewBag.sa = _context.SecteurActivites.ToList();
            return View();
        }

        public IActionResult List()
        {
            List<Client> liste = _context.Clients.Include(c => c.SecteurActivite).ToList();
            return View(liste);
        }

        [HttpPost]
        public IActionResult New(Client c, string notes)
        {
            // QUESTION YVES
            // j'ai un soucis sur ce ModelState. bloque à cause de SecteurActivite
            // Il est toujorus false si je remplis les champs car il prend secteurActiviteID
            // J'ai obtenu la solution ModelState.Remove de Claude et ça marche comme ça mais pas propre et pas compris
            ModelState.Remove("SecteurActivite");
            if (ModelState.IsValid)
            {
                _context.Clients.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Form", "Question", new {newClient = c.Id, notes = notes });
            }
            else
            {
                ViewBag.sa = _context.SecteurActivites.ToList();
                return View(c);
            }

        }
        [HttpGet]
        public IActionResult Update(Client c)
        {
            Client existClient = _context.Clients.FirstOrDefault(c => c.Id == c.Id);
            ViewBag.sa = _context.SecteurActivites.ToList();
            return View(existClient);
        }

        [HttpPost]
        public IActionResult UpdateDB(Client c)
        {
            ModelState.Remove("SecteurActivite");
            if (ModelState.IsValid)
            {
                _context.Clients.Update(c);
                _context.SaveChanges();
                return RedirectToAction("Menu", "MainMenu");
            }
            else
            {
                ViewBag.sa = _context.SecteurActivites.ToList();
                return RedirectToAction("Update", "Client");
            }

        }
    }
}
