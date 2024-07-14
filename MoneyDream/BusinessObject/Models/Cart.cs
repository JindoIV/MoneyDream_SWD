using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Cart
{
    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
