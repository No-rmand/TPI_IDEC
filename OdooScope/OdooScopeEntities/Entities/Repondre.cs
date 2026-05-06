using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class Repondre
{
    public int Id { get; set; }

    public bool Reponse { get; set; }

    public int ClientId { get; set; }

    public int QuestionId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
