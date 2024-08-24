using System;
using System.Collections.Generic;

namespace cinema.Models;

public partial class Combo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<ComboDetail> ComboDetails { get; set; } = new List<ComboDetail>();
}
