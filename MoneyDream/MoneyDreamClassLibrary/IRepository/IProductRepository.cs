using MoneyDreamClassLibrary.DataAccess;

namespace MoneyDreamClassLibrary.IRepository;

public interface IProductRepository
{
    // public void AddProduct(Product product);
    public IEnumerable<Product> GetAllProducts(int pageNumber = 1, int pageSize = 5);
    public (IEnumerable<object>, int totalRecord ) GetAllProductCatalog(int pageNumber = 1, int pageSize = 5);

    public Product GetProductById(int id);
    public object GetProductInfoById(int id);
    // public void UpdateProduct(Product product);
    // public void DeleteProduct(int id);
    // public IEnumerable<Product> SearchProducts(string? name, int? category, int pageNumber = 1, int pageSize = 5);
    // public int GetCount();

    public List<Category> GetListCategory();
    public List<Supplier> GetSupliers();

    public Category GetCategoryById(int id);
    public List<object> GetProductByCategoryId(int id);

}