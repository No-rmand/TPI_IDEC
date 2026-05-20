using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeWeb.Controllers
{
    public class QuestionController : Controller
    {
        private SqlServerContext _context;
        public QuestionController(SqlServerContext context)
        {
            _context = context;
        }
        public IActionResult New()
        {
            List<Question> list = _context.Questions.OrderBy(q => q.Ordre).ToList();
            return View(list);
            // YVES j'ai besoin de formation sur JS pour:
            // masquer les QuestionId == null
            // les afficher si réponse = Oui
        }

        public IActionResult List()
        {
            List<Question> liste = _context.Questions.Include(q => q.SecteurActivite).ToList();
            return View(liste);

        }
        [HttpPost]
        public IActionResult Form(int clientId, string notes, List<int> questionIds, List<bool> reponses)
        {

            for (int i = 0; i < questionIds.Count; i++)
            {
                Repondre r = new Repondre
                {
                    ClientId = clientId,
                    QuestionId = questionIds[i],
                    Reponse = reponses[i],
                };
                _context.Repondres.Add(r);
            }
            _context.SaveChanges();

            return RedirectToAction("Result", "Resultat", new { clientId = clientId, note = notes});
        }

        [HttpGet]
        public IActionResult Form(int newClient, string notes)
        {
            List<Question> questionnaire = _context.Questions.Include(q => q.SecteurActivite).OrderBy(q => q.Ordre).ToList();
            ViewBag.NewClient = newClient;
            ViewBag.Notes = notes;
            return View(questionnaire);
        }
    }
}
