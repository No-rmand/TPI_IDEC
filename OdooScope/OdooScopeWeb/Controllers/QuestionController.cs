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

            // QUESTION YVES
            // Comment faire pour que la clonne Question Parent affiche non pas l'ID (QuestionId) mais le texte de Question (Question.texte)
            // J'ai essayé .Include(q => q.Questions.Texte) mais j'ai une erreur de compil sur Questions

            // + D'ou vien Question(s) alors que dans ma db la table s'appele bien Question ???
        }
        public IActionResult Form(string notes)
        {
            ViewBag.Notes = notes;
            List<Question> liste = _context.Questions.ToList();
            return View(liste);
        }
    }
}
