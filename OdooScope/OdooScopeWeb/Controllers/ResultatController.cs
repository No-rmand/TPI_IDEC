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
        public IActionResult Result(int clientId, string notes)
        {
            Resultat resultat = _context.Resultats.Include(r => r.Client).ThenInclude(s => s.SecteurActivite).FirstOrDefault(r => r.ClientId == clientId);

            List<CreationListe> appOdoo = _context.CreationListes.Where(cl => cl.ResultatId == resultat.Id).Include(cl => cl.ApplicationOdoo).ToList();

            ViewBag.Applications = appOdoo;

            return View(resultat);
        }

        public IActionResult NewClient(Client c, string notes)
        {
            List<Client> liste = _context.Clients.Include(c => c.SecteurActivite).ToList();
            return View(liste);
        }

        [HttpPost]
        public IActionResult UpdateNotes(int resultatId, string notes)
        {
            Resultat resultat = _context.Resultats.FirstOrDefault(r => r.Id == resultatId);
            resultat.Notes = notes;
            _context.Resultats.Update(resultat);
            _context.SaveChanges();
            return RedirectToAction("Result", new { clientId = resultat.ClientId, notes = notes });
        }

    }
}
