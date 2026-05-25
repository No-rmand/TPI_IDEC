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
            List<Question> liste = _context.Questions.ToList();
            return View(liste);

        }

        [HttpGet]
        public IActionResult Form(int newClient, string notes)
        {
            List<Question> questionnaire = _context.Questions.OrderBy(q => q.Ordre).ToList();
            ViewBag.NewClient = newClient;
            ViewBag.Notes = notes;
            return View(questionnaire);
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

            Client client = _context.Clients.FirstOrDefault(c => c.Id == clientId);

            List<Repondre> reponduOui = _context.Repondres.Where(c => c.ClientId == clientId && c.Reponse == true).ToList();

            List<int> appOdoo = new List<int>();

            foreach (Repondre r in reponduOui)
            {
                List<QuestionApplication> qApp = _context.QuestionApplications
                    .Where(qa => qa.QuestionId == r.QuestionId)
                    .Include(qa => qa.ApplicationOdoo)
                    .ToList();

                foreach (QuestionApplication qa in qApp)
                {
                    if (qa.ApplicationOdooId != null &&
                        (qa.ApplicationOdoo.EmployeMin == null || qa.ApplicationOdoo.EmployeMin <= client.NombreEmploye))
                    {
                        appOdoo.Add(qa.ApplicationOdooId.Value);
                    }
                }
            }


            List<ApplicationOdoo> appEssentielles = _context.ApplicationOdoos
                .Where(sa => sa.EstEssentiel == true &&(sa.SecteurActiviteId == null || sa.SecteurActiviteId == client.SecteurActiviteId) &&(sa.EmployeMin == null || sa.EmployeMin <= client.NombreEmploye)).ToList();

            foreach (ApplicationOdoo app in appEssentielles)
            {
                appOdoo.Add(app.Id);
            }

            Resultat resultat = new Resultat
            {
                ClientId = clientId,
                DateGeneration = DateOnly.FromDateTime(DateTime.Now),
                Notes = notes
            };

            _context.Resultats.Add(resultat);
            _context.SaveChanges();

            appOdoo = appOdoo.Distinct().ToList();

            foreach (int appId in appOdoo)
            {
                _context.CreationListes.Add(new CreationListe
                {
                    ResultatId = resultat.Id,
                    ApplicationOdooId = appId
                });
            }
            _context.SaveChanges();

            return RedirectToAction("Result", "Resultat", new { clientId = clientId, notes = notes});
        }


    }
}
