using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
