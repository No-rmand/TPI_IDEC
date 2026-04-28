using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class Resultat
{
    public int Id { get; set; }

    public DateOnly DateGeneration { get; set; }

    public string? Notes { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<CreationListe> CreationListes { get; set; } = new List<CreationListe>();
}
