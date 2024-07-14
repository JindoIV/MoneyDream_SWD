using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class ProductImage
{
    public int ImageId { get; set; }

    public string Image { get; set; } = null!;

    public int ProductId { get; set; }

    public string? PublicId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
