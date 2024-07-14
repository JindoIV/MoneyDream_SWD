using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public int UnitId { get; set; }

    public int SupplierId { get; set; }

    public int? ImportId { get; set; }

    public int? OldPrice { get; set; }

    public int SizeId { get; set; }

    public int Discount { get; set; }

    public string? Status { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? PublicId { get; set; }

    public string? ImageUrlcloud { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ExportInfo> ExportInfos { get; set; } = new List<ExportInfo>();

    public virtual ImportInfo? Import { get; set; }

    public virtual ICollection<ImportInfo> ImportInfos { get; set; } = new List<ImportInfo>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual Size Size { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
