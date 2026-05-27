using Microsoft.AspNetCore.Mvc;
using OdooScopeEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdooScopeWeb.Controllers
{
    public class QuestionnaireData
    {
        public int ClientId { get; set; }
        public string Notes { get; set; }
        public List<int> QuestionIds { get; set; }
        public List<bool> Reponses { get; set; }
    }

    public class QuestionController : Controller
    {
        private SqlServerContext _context;
        public QuestionController(SqlServerContext context)
        {
            _context = context;
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
        public IActionResult Form([FromBody] QuestionnaireData data)
        {
            for (int i = 0; i < data.QuestionIds.Count; i++)
            {
                _context.Repondres.Add(new Repondre
                {
                    ClientId = data.ClientId,
                    QuestionId = data.QuestionIds[i],
                    Reponse = data.Reponses[i]
                });
            }
            _context.SaveChanges();

            Client client = _context.Clients.FirstOrDefault(c => c.Id == data.ClientId);

            List<Repondre> reponduOui = _context.Repondres
                .Where(r => r.ClientId == data.ClientId && r.Reponse == true)
                .ToList();

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
                .Where(a => a.EstEssentiel == true &&
                       (a.SecteurActiviteId == null || a.SecteurActiviteId == client.SecteurActiviteId) &&
                       (a.EmployeMin == null || a.EmployeMin <= client.NombreEmploye))
                .ToList();

            foreach (ApplicationOdoo app in appEssentielles)
            {
                appOdoo.Add(app.Id);
            }

            appOdoo = appOdoo.Distinct().ToList();

            Resultat resultat = new Resultat
            {
                ClientId = data.ClientId,
                DateGeneration = DateOnly.FromDateTime(DateTime.Now),
                Notes = data.Notes
            };

            _context.Resultats.Add(resultat);
            _context.SaveChanges();

            foreach (int appId in appOdoo)
            {
                _context.CreationListes.Add(new CreationListe
                {
                    ResultatId = resultat.Id,
                    ApplicationOdooId = appId
                });
            }
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            List<Question> questionnaire = _context.Questions.OrderBy(q => q.Ordre).ToList();
            List<Repondre> reponsesExistantes = _context.Repondres.Where(r => r.ClientId == id).ToList();
            Client client = _context.Clients.Include(c => c.SecteurActivite).FirstOrDefault(c => c.Id == id);
            Resultat resultat = _context.Resultats.FirstOrDefault(r => r.ClientId == id);

            ViewBag.Client = client;
            ViewBag.ClientId = id;
            ViewBag.Notes = resultat?.Notes;
            ViewBag.ReponsesExistantes = reponsesExistantes;

            return View(questionnaire);
        }

        [HttpPost]
        public IActionResult Update([FromBody] QuestionnaireData data)
        {
            List<Repondre> anciennes = _context.Repondres
                .Where(r => r.ClientId == data.ClientId)
                .ToList();
            _context.Repondres.RemoveRange(anciennes);
            _context.SaveChanges();

            for (int i = 0; i < data.QuestionIds.Count; i++)
            {
                _context.Repondres.Add(new Repondre
                {
                    ClientId = data.ClientId,
                    QuestionId = data.QuestionIds[i],
                    Reponse = data.Reponses[i]
                });
            }
            _context.SaveChanges();

            Client client = _context.Clients.FirstOrDefault(c => c.Id == data.ClientId);

            List<Repondre> reponduOui = _context.Repondres
                .Where(r => r.ClientId == data.ClientId && r.Reponse == true)
                .ToList();

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
                .Where(a => a.EstEssentiel == true &&
                       (a.SecteurActiviteId == null || a.SecteurActiviteId == client.SecteurActiviteId) &&
                       (a.EmployeMin == null || a.EmployeMin <= client.NombreEmploye))
                .ToList();

            foreach (ApplicationOdoo app in appEssentielles)
            {
                appOdoo.Add(app.Id);
            }

            appOdoo = appOdoo.Distinct().ToList();

            Resultat ancienResultat = _context.Resultats.FirstOrDefault(r => r.ClientId == data.ClientId);
            if (ancienResultat != null)
            {
                List<CreationListe> anciennesListes = _context.CreationListes
                    .Where(cl => cl.ResultatId == ancienResultat.Id)
                    .ToList();
                _context.CreationListes.RemoveRange(anciennesListes);
                ancienResultat.Notes = data.Notes;
                ancienResultat.DateGeneration = DateOnly.FromDateTime(DateTime.Now);
                _context.Resultats.Update(ancienResultat);
                _context.SaveChanges();

                foreach (int appId in appOdoo)
                {
                    _context.CreationListes.Add(new CreationListe
                    {
                        ResultatId = ancienResultat.Id,
                        ApplicationOdooId = appId
                    });
                }
                _context.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}