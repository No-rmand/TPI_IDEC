using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int NombreEmploye { get; set; }

    public int SecteurActiviteId { get; set; }

    public virtual ICollection<Repondre> Repondres { get; set; } = new List<Repondre>();

    public virtual ICollection<Resultat> Resultats { get; set; } = new List<Resultat>();

    public virtual SecteurActivite SecteurActivite { get; set; } = null!;
}
