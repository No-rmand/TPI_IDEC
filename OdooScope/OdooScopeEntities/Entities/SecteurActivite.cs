using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class SecteurActivite
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ApplicationOdoo> ApplicationOdoos { get; set; } = new List<ApplicationOdoo>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
