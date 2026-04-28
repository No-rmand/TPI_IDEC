using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class Question
{
    public int Id { get; set; }

    public string Texte { get; set; } = null!;

    public int Ordre { get; set; }

    public int? QuestionId { get; set; }

    public int? SecteurActiviteId { get; set; }

    public bool? Reponse { get; set; }

    public virtual ICollection<Question> InverseQuestionNavigation { get; set; } = new List<Question>();

    public virtual ICollection<QuestionApplication> QuestionApplications { get; set; } = new List<QuestionApplication>();

    public virtual Question? QuestionNavigation { get; set; }

    public virtual SecteurActivite? SecteurActivite { get; set; }
}
