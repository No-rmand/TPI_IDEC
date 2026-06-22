using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



        public IActionResult List()
        {
            List<Client> liste = _context.Clients.Include(c => c.SecteurActivite).ToList();
            return View(liste);
        }



        [HttpGet]
        public IActionResult New()
        {
            ViewBag.sa = _context.SecteurActivites.ToList();
            return View();
        }

        // Enregistre le client en DB et trasfert la note à la page suivante
        [HttpPost]
        public IActionResult New(Client c, string notes)
        {
            ModelState.Remove("SecteurActivite");
            if (ModelState.IsValid)
            {
                _context.Clients.Add(c);
                _context.SaveChanges();
                TempData["ok"] = "Client créé avec succès.";
                return RedirectToAction("Form", "Question", new { newClient = c.Id, notes = notes });
            }
            else
            {
                ViewBag.sa = _context.SecteurActivites.ToList();
                TempData["ko"] = "Veuillez corriger les erreurs.";
                return View(c);
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            Client existClient = _context.Clients.FirstOrDefault(c => c.Id == id);
            Resultat resultat = _context.Resultats.FirstOrDefault(r => r.ClientId == id);
            ViewBag.sa = _context.SecteurActivites.ToList();
            ViewBag.Notes = resultat?.Notes;
            return View(existClient);
        }

        [HttpPost]
        public IActionResult Update(Client c, string notes)
        {
            ModelState.Remove("SecteurActivite");
            if (ModelState.IsValid)
            {
                _context.Clients.Update(c);

                Resultat notesResultat = _context.Resultats.FirstOrDefault(r => r.ClientId == c.Id);
                if(notesResultat != null)
                {
                    notesResultat.Notes = notes;
                    _context.Resultats.Update(notesResultat);
                }
                _context.SaveChanges();
                TempData["ok"] = "Client mis à jour avec succès.";
                return RedirectToAction("Menu", "MainMenu");
            }
            else
            {
                ViewBag.sa = _context.SecteurActivites.ToList();
                ViewBag.Notes = notes;
                TempData["ko"] = "Veuillez corriger les erreurs.";
                return RedirectToAction("Update", c);
            }
        }
    }
}
