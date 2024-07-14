using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class AccountAddress
{
    public int AddressId { get; set; }

    public string DeliveryName { get; set; } = null!;

    public string DeliveryPhone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? DeleteAt { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
