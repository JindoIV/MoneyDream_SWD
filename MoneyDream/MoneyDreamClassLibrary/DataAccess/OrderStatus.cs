using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusCode { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
