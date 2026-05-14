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
            return View();
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
        public IActionResult Form()
        {
            List<Question> liste = _context.Questions.ToList();
            return View(liste);
        }
    }
}
