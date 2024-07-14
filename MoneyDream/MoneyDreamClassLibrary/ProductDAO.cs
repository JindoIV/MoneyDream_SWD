using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoneyDreamClassLibrary.DataAccess;

namespace MoneyDreamClassLibrary;

public class ProductDAO
{
    private static ProductDAO _instance;
    private static readonly object _lock = new object();
    private readonly MoneyDreamContext _context;

    private ProductDAO()
    {
        _context = new MoneyDreamContext();
    }

    public static ProductDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductDAO();
                    }
                }
            }
            return _instance;
        }
    }



    public Product GetProductById(int id)
    {
        Product product = _context.Products
                            .Include(x => x.Import)
                            .Include(x => x.Size)
                            .Include(x => x.ProductImages)
                            .FirstOrDefault(x => x.ProductId == id);
                            
        return product;
    }

    public object GetProductInfoById(int id)
    {
        try
        {
            var context = new MoneyDreamContext();


            var keyProduct = (from p in context.Products
                         where p.ProductId == id 
                         select p).FirstOrDefault();

            var productInStock = GetProductInStock(keyProduct);

            var size = (from p in context.Products
                        join s in context.Sizes on p.SizeId equals s.SizeId
                        where p.Name == keyProduct.Name && p.CategoryId == keyProduct.CategoryId
                        select new
                        {
                            p.ProductId,
                            s.SizeId,
                            s.Name,
                            s.ProductWidth
                        }).ToList();

            var proImages = (from pi in context.ProductImages
                             join p in context.Products on pi.ProductId equals p.ProductId
                             where p.ProductId == id
                             select new
                             {
                                pi.Image,
                             }).ToList();
            var supplier = context.Suppliers.SingleOrDefault(x => x.SupplierId == keyProduct.SupplierId).Name;
            var exportPrice = context.ImportInfos.SingleOrDefault(x => x.ImportId == keyProduct.ImportId).ExportPrice;

            return new
            {
                keyProduct.ProductId,
                keyProduct.Name,
                keyProduct.CategoryId,
                keyProduct.SupplierId,
                supplier,
                keyProduct.ImportId,
                exportPrice,
                keyProduct.OldPrice,
                keyProduct.SizeId,
                keyProduct.Discount,
                keyProduct.Status,
                keyProduct.PublicId,
                keyProduct.ImageUrlcloud,
                proImages,
                size
            };
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<object> GetProductByCategoryId(int id)
    {
        var response = new List<object>(); 
       try
        {
            var context = new MoneyDreamContext();

            List<Product> listPorduct = context.Products.Where(x => x.Status == "Enable" && x.CategoryId == id).ToList();

            var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                      .Select(g => g.First())
                                      .Where(x => x.Status == "Enable")
                                      .ToList();

            foreach (var item in ProductNameUnique)
            {
                var supplier = context.Suppliers.SingleOrDefault(x => x.SupplierId == item.SupplierId).Name;
                var exportPrice = _context.ImportInfos.SingleOrDefault(x => x.ImportId == item.ImportId).ExportPrice;
                var keyProduct = (from p in _context.Products
                                  where p.ProductId == item.ProductId
                                  select p).FirstOrDefault();


                var size = (from p in context.Products
                            join s in context.Sizes on p.SizeId equals s.SizeId
                            where p.Name == keyProduct.Name && p.CategoryId == keyProduct.CategoryId
                            select new
                            {
                                p.ProductId,
                                s.SizeId,
                                s.Name,
                                s.ProductWidth
                            }).ToList();

                var proImages = (from pi in context.ProductImages
                                 join p in context.Products on pi.ProductId equals p.ProductId
                                 where p.ProductId == item.ProductId
                                 select new
                                 {
                                     pi.Image,
                                 }).ToList();

                response.Add(new
                {
                    keyProduct.ProductId,
                    keyProduct.Name,
                    keyProduct.CategoryId,
                    keyProduct.SupplierId,
                    supplier,
                    keyProduct.ImportId,
                    exportPrice,
                    keyProduct.OldPrice,
                    keyProduct.SizeId,
                    keyProduct.Discount,
                    keyProduct.Status,
                    keyProduct.PublicId,
                    keyProduct.ImageUrlcloud,
                    proImages,
                    size
                }) ;
            }
            return response;
        } catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.Where(x => x.Status.Equals("Enable"))
                    .Include(x => x.Category)
                    .Include(x => x.ImportInfos)
                    .ToList();
    }

    public (IEnumerable<object>, int) GetAllProductCatalog(int pageNumber, int pageSize)
    {
        var response = new List<object>();
        try
        {
            var context = new MoneyDreamContext();
            List<Product> listPorduct = context.Products.Where(x => x.Status == "Enable").ToList();

            var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                      .Select(g => g.First())
                                      .Where(x => x.Status == "Enable")
                                      .ToList();

            var totalRecord = ProductNameUnique.Count();

            foreach (var product in ProductNameUnique) {

                var supplier = context.Suppliers.SingleOrDefault(x => x.SupplierId == product.SupplierId).Name;
                var exportPrice = context.ImportInfos.SingleOrDefault(x => x.ImportId == product.ImportId).ExportPrice;
                var size = (from p in context.Products
                            join s in context.Sizes on p.SizeId equals s.SizeId
                            where p.Name == product.Name && p.CategoryId == product.CategoryId
                            select new
                            {
                                p.ProductId,
                                s.SizeId,
                                s.Name,
                                s.ProductWidth
                            }).ToList();

                var proImages = (from pi in context.ProductImages
                                 join p in context.Products on pi.ProductId equals p.ProductId
                                 where p.ProductId == product.ProductId
                                 select new
                                 {
                                     pi.Image,
                                 }).ToList();

                response.Add(new
                {
                    product.ProductId,
                    product.Name,
                    product.CategoryId,
                    product.SupplierId,
                    supplier,
                    product.ImportId,
                    exportPrice,
                    product.OldPrice,
                    product.SizeId,
                    product.Discount,
                    product.Status,
                    product.PublicId,
                    product.ImageUrlcloud,
                    proImages,
                    size
                });
            }

            return (response, totalRecord);
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }



    // public IEnumerable<Product> SearchProducts(string? name, int? category, int pageNumber = 1, int pageSize = 5)
    // {
    //     if (string.IsNullOrEmpty(name) && !category.HasValue)
    //     {
    //         return GetAllProducts(pageNumber, pageSize);
    //     }

    //     // Start with all products
    //     var products = _context.Products.AsQueryable();

    //     // Filter by name if provided
    //     if (!string.IsNullOrEmpty(name))
    //     {
    //         products = products.Where(p => p.Name.Contains(name));
    //     }

    //     // Filter by category if provided
    //     if (category.HasValue)
    //     {
    //         products = products.Where(p => p.CategoryId == category.Value);
    //     }


    //     return Pagination(products.Where(x => x.ProductAvailable == true).ToList(), pageNumber, pageSize);
    // }

    public List<Category> GetListCategory()
    {
        List<Category> listCategory = new List<Category>();
        try
        {
            using (var DbContext = new MoneyDreamContext())
            {
                listCategory = DbContext.Categories.ToList();
            }
        }
        catch (Exception)
        {
            throw new Exception("Get list categorys fail!");
        }
        return listCategory;
    }

    public Category? GetCategoryById(int id)
    {
        Category? category = new Category();
        try
        {
            using (var DbContext = new MoneyDreamContext())
            {
                category = DbContext.Categories.SingleOrDefault(x => x.CategoryId == id);
            }
        }
        catch (Exception)
        {
            throw new Exception("Get category by id fail!");
        }
        return category;
    }

    public List<Supplier> GetListSupplier()
    {
        List<Supplier> sup = new List<Supplier>();
        try
        {
            using (var DbContext = new MoneyDreamContext())
            {
                sup = DbContext.Suppliers.ToList();
            }
        }
        catch (Exception)
        {
            throw new Exception("Get list supplier fail!");
        }
        return sup;
    }

    public int GetProductInStock(Product product) { 
        try
        {
            var context = new MoneyDreamContext();
            var GetImport = context.ImportInfos.Include(x => x.Product)
                                            .Include(x => x.Product.Supplier)
                                            .Include(x => x.Product.Size)
                                            .Include(x => x.Product.Category)
                                            .SingleOrDefault(x => x.ImportId == product.ImportId);

                var ListExportDetail = context.ExportInfos.Include(x => x.Account)
                                                 .Include(x => x.Product)
                                                 .Include(x => x.Product.Supplier)
                                                 .Include(x => x.Product.Category)
                                                 .Include(x => x.Product.Size)
                                                 .Include(x => x.Import)
                                                 .Where(x => x.ImportId == product.ImportId).ToList();


            return ((int)GetImport!.Count - (int)ListExportDetail?.Sum(x => x.Count)!);
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<ImportInfo> GetListImportByProductId(int? id)
    {
        List<ImportInfo> import = new List<ImportInfo>();
        try
        {
            using (var DbContext = new MoneyDreamContext())
            {
                import = DbContext.ImportInfos.Include(x => x.Product)
                    .Include(x => x.Product.Supplier)
                    .Include(x => x.Product.Category)
                    .Include(x => x.Product.Size)
                    .Where(x => x.ProductId == id).ToList();
            }
        }
        catch (Exception)
        {
            throw new Exception("Get import by product id fail!");
        }
        return import;
    }
    public List<ExportInfo> GetListExportByProductId(int id)
    {
        List<ExportInfo> export = new List<ExportInfo>();
        try
        {
            using (var DbContext = new MoneyDreamContext())
            {
                export = DbContext.ExportInfos.Where(x => x.ProductId == id).ToList();
            }
        }
        catch (Exception)
        {
            throw new Exception("Get export by product id fail!");
        }
        return export;
    }


}
