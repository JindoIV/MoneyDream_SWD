using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = null!;

    public string? Created { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
