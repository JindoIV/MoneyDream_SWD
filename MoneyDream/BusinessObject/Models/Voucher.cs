using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public decimal Discount { get; set; }

    public string VoucherName { get; set; } = null!;

    public string CreateAt { get; set; } = null!;

    public string ExpiredAt { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
