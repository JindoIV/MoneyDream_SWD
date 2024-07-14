using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;

namespace MoneyDreamClassLibrary.Repository;

public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAllProducts(int pageNumber = 1, int pageSize = 5)
    => ProductDAO.Instance.GetAllProducts();

    public Product GetProductById(int id)
    => ProductDAO.Instance.GetProductById(id);

    public List<Category> GetListCategory() => ProductDAO.Instance.GetListCategory();
 

    public Category GetCategoryById(int id) => ProductDAO.Instance.GetCategoryById(id)!;
    public List<object> GetProductByCategoryId(int id) => ProductDAO.Instance.GetProductByCategoryId(id)!;

    public object GetProductInfoById(int id) => ProductDAO.Instance.GetProductInfoById(id);

    public (IEnumerable<object>, int totalRecord) GetAllProductCatalog(int pageNumber = 1, int pageSize = 5)
    => ProductDAO.Instance.GetAllProductCatalog(pageNumber, pageSize);

    public List<Supplier> GetSupliers() => ProductDAO.Instance.GetListSupplier();
}