using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class EmailToken
{
    public int EmailTokenId { get; set; }

    public string Token { get; set; } = null!;

    public string? Created { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
