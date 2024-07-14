using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductWidth { get; set; } = null!;

    public string ProductHeight { get; set; } = null!;

    public string SampleHeight { get; set; } = null!;

    public string SampleWeight { get; set; } = null!;

    public string? Status { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
