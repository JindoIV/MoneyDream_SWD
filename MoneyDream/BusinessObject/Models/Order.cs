using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int AccountId { get; set; }

    public int? PaymentId { get; set; }

    public int AddressId { get; set; }

    public int? VoucherId { get; set; }

    public int OrderStatusId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? OrderDate { get; set; }

    public string? UpdateStatusAt { get; set; }

    public string? Notes { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual AccountAddress Address { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
