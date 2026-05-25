using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class Question
{
    public int Id { get; set; }

    public string Texte { get; set; } = null!;

    public int Ordre { get; set; }

    public int? QuestionId { get; set; }

    public virtual ICollection<QuestionApplication> QuestionApplications { get; set; } = new List<QuestionApplication>();

    public virtual ICollection<Repondre> Repondres { get; set; } = new List<Repondre>();
}
