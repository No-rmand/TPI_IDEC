using System;
using System.Collections.Generic;

namespace OdooScopeEntities.Entities;

public partial class QuestionApplication
{
    public int Id { get; set; }

    public int? ApplicationOdooId { get; set; }

    public int? QuestionId { get; set; }

    public virtual ApplicationOdoo? ApplicationOdoo { get; set; }

    public virtual Question? Question { get; set; }
}
