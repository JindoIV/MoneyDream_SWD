using System;
using System.Collections.Generic;

namespace MoneyDreamClassLibrary.DataAccess;

public partial class ImportInfo
{
    public int ImportId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public int ImportPrice { get; set; }

    public int ExportPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DateImport { get; set; }

    public virtual ICollection<ExportInfo> ExportInfos { get; set; } = new List<ExportInfo>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
