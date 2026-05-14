using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class CreationListe
{
    public int Id { get; set; }

    public int? ResultatId { get; set; }

    public int? ApplicationOdooId { get; set; }

    public virtual ApplicationOdoo? ApplicationOdoo { get; set; }

    public virtual Resultat? Resultat { get; set; }
}
