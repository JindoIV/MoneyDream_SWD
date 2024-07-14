using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int Type { get; set; }

    public decimal? Amount { get; set; }

    public string PaymentDateTime { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual PaymentType TypeNavigation { get; set; } = null!;
}
