using MoneyDreamAPI.Dto.PaginationDto;
using MoneyDreamAPI.Dto.ProductDto;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using Size = MoneyDreamAPI.Dto.ProductDto.Size;

namespace MoneyDreamAPI;

public interface IProductService
{
    public object GetAllProductCustomer(PaginationRequest request); //Product List
    public object GetAllProductCatalog(PaginationRequest request); //Product List
    public object GetProduct(int id);    
    public object GetProductByID(int id);
    //Product detail
    // public object GetProductAdmin(int id);

    public object GetAllCategory();
    public object GetAllSuppliers();
    public object GetCategoryById(int categoryID);
    public object GetProductByCategoryID(int categoryID);

}

public class ProductService : IProductService
{

    private readonly IProductRepository _repository;

    public ProductService()
    {
        _repository = new ProductRepository();
    }

    public object GetAllCategory()
    {
        return _repository.GetListCategory();
    }

    public object GetAllProductCatalog(PaginationRequest request)
    {
        (IEnumerable<object> orders, int totalRecord) = _repository.GetAllProductCatalog(request.PageNumber, request.PageSize);
        return new
        {
            paginationData = new
            {
                totalPage = (int)Math.Ceiling((double)totalRecord / request.PageSize),
                totalRecord = totalRecord,
                pageNumber = request.PageNumber,
                pageSize = request.PageSize,
                pageData = orders
            }
        };
    }

    public object GetAllProductCustomer(PaginationRequest request)
    {
        int pageNumber = request.PageNumber;
        int pageSize = request.PageSize;

        // Get all products and count
        IEnumerable<Product> products = _repository.GetAllProducts();
        int totalRecord = products.Count();

        // Pagination
        products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var allProductsResponse = products.Select(product => new ProductHomePageResponse
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,
            Name = product.Name,
            Discount = product.Discount,
            ImageUrl = product.ImageUrlcloud,
            Price = product.ImportInfos?.Select(x => x.ExportPrice).LastOrDefault() ?? 0,
            OldPrice = product.OldPrice
        }).ToList();

        int totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);

        return new
        {
            totalPage,
            totalRecord,
            pageNumber,
            pageSize,
            pageData = allProductsResponse
        };
    }

    public object GetAllSuppliers()
    {
        return _repository.GetSupliers();
    }

    public object GetCategoryById(int categoryID)
    {
       return _repository.GetCategoryById(categoryID);
    }

    public object GetProduct(int id)
    {
        Product product = _repository.GetProductById(id);

        if (product == null)
        {
            return null;
        }

        var imagesUrl = product.ProductImages?.Select(image => image.Image).ToList() ?? new List<string>();

        Size size = null;
        if (product.Size != null)
        {
            size = new Size
            {
                SizeId = product.Size.SizeId,
                Name = product.Size.Name,
                ProductWidth = product.Size.ProductWidth,
                ProductHeight = product.Size.ProductHeight,
                SampleHeight = product.Size.SampleHeight,
                SampleWeight = product.Size.SampleWeight,
                Status = product.Size.Status,
                Description = product.Size.Description
            };
        }

        var pageData = new ProductCustomerResponse
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,
            Name = product.Name,
            Discount = product.Discount,
            ImageMainUrl = product.ImageUrlcloud,
            Price = product.ImportInfos?.Select(x => x.ExportPrice).LastOrDefault() ?? 0,
            OldPrice = product.OldPrice,
            Count = product.Import?.Count ?? 0,
            ImageUrl = imagesUrl,
            Status = product.Status ?? string.Empty,
            Size = size
        };

        return new { pageData };
    }

    public object GetProductByCategoryID(int categoryID)
    {
        return _repository.GetProductByCategoryId(categoryID);
    }

    public object GetProductByID(int id)
    {
        return _repository.GetProductInfoById(id);
    }



    // public object GetProductAdmin(int id)
    // {

    //     Product product = _repository.GetProductById(id);

    //     if (product == null)
    //     {
    //         return null;
    //     }

    //     IEnumerable<ImportInfo> importInfos = product.ImportInfos;
    //     int price = importInfos.Select(x => x.ExportPrice).LastOrDefault();

    //     ICollection<string> imagesUrl = new List<string>();

    //     foreach (var image in product.ProductImages)
    //     {
    //         imagesUrl.Add(image.Image);
    //     }

    //     var pageData = new ProductCustomerResponse
    //     {
    //         ProductId = product.ProductId,
    //         CategoryId = product.CategoryId,
    //         CategoryName = product.Category.Name,
    //         Name = product.Name,
    //         Discount = product.Discount,
    //         ImageMainUrl = product.ImageUrl,
    //         Price = price,
    //         OldPrice = product.OldPrice,
    //         ImageUrl = imagesUrl

    //     };

    //     return new { pageData = product};
    // }

    private IEnumerable<Product> Pagination(IEnumerable<Product> products, int pageNumber = 1, int pageSize = 5)
    {
        return products.OrderByDescending(on => on.ProductId)
                       .Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
    }

}
