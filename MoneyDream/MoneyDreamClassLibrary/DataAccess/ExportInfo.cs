using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class ExportInfo
{
    public int ExportId { get; set; }

    public int ProductId { get; set; }

    public int ImportId { get; set; }

    public int AccountId { get; set; }

    public int Count { get; set; }

    public DateTime? DateExport { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ImportInfo Import { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
