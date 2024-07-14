using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetListProduct() => ProductDAO.Instance.GetListProduct();
        public void CreateProduct(Product product) => ProductDAO.Instance.CreateProduct(product);
        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id)!;
        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
        public Product GetProductByName(string name) => ProductDAO.Instance.GetProductByName(name)!;
        public void DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);
        public Product GetUniqueProduct(string name, int supplierId, int categoryId, int sizeId) => ProductDAO.Instance.GetUniqueProduct(name, supplierId, categoryId, sizeId)!;
    }
}
