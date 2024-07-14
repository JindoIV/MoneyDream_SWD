using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        private ProductDAO() { }
        private static ProductDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetListProduct()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listProduct = DbContext.Products.Include(x => x.Category)
                        .Include(x => x.Supplier)
                        .Include(x => x.Unit)
                        .Include(x => x.Size)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list products fail!");
            }
            return listProduct;
        }

        public Product? GetProductByName(string name)
        {
            Product? product = new Product();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    product = DbContext.Products.FirstOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get product by name fail!");
            }
            return product;
        }

        public Product? GetUniqueProduct(string name, int supplierId, int categoryId, int sizeId)
        {
            Product? product = new Product();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    product = DbContext.Products.Include(x => x.Category)
                        .Include(x => x.Supplier)
                        .Include (x => x.Unit)
                        .Include(x => x.Size) .FirstOrDefault(x => x.Name == name && 
                                                                x.SupplierId == supplierId &&
                                                                x.CategoryId == categoryId &&
                                                                x.SizeId == sizeId);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get product by name and old price fail!");
            }
            return product;
        }

        public Product? GetProductById(int id)
        {
            Product? product = new Product();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    product = DbContext.Products.Include(x => x.Category)
                        .Include(x => x.Supplier)
                        .Include(x => x.Unit)
                        .Include(x => x.Size).FirstOrDefault(x => x.ProductId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get product by id fail!");
            }
            return product;
        }

        public void CreateProduct(Product product)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Products.Add(product);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create product fail!");
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Product>(product).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update product fail!");
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    Product? product = new Product();
                    product = DbContext.Products.SingleOrDefault(x => x.ProductId == id);
                    DbContext.Products.Remove(product!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete product fail!");
            }
        }
    }
}
