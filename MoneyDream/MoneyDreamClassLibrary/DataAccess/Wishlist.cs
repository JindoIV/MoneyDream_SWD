using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int AccountId { get; set; }

    public int ProductId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;
}
