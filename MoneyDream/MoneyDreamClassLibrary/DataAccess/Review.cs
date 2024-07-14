using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public string? ReviewContent { get; set; }

    public int? Rate { get; set; }

    public string? CreateAt { get; set; }

    public virtual Account? Account { get; set; }
}
