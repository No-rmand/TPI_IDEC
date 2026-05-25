using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class ApplicationOdoo
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool EstEssentiel { get; set; }

    public bool EstAdministrative { get; set; }

    public int? SecteurActiviteId { get; set; }

    public int? EmployeMin { get; set; }

    public virtual ICollection<CreationListe> CreationListes { get; set; } = new List<CreationListe>();

    public virtual ICollection<QuestionApplication> QuestionApplications { get; set; } = new List<QuestionApplication>();

    public virtual SecteurActivite? SecteurActivite { get; set; }
}
