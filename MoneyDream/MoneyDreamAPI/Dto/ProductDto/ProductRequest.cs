namespace MoneyDreamAPI.Dto.ProductDto
{
    public class ProductCustomerResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        public string ImageMainUrl { get; set; } = null!;
        public ICollection<string>? ImageUrl { get; set; }
        public string Status { get; set; } = null!;
        public Size Size { get; set; }
    }

    public class Size
    {
        public int SizeId { get; set; }

        public string Name { get; set; } = null!;

        public string ProductWidth { get; set; } = null!;

        public string ProductHeight { get; set; } = null!;

        public string SampleHeight { get; set; } = null!;

        public string SampleWeight { get; set; } = null!;

        public string? Status { get; set; }

        public string? Description { get; set; }
    }

    public class ProductHomePageResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { get; set; } = null!;

    }
}
