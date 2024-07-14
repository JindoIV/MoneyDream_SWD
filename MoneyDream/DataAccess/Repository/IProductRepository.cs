using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetListProduct();
        public void CreateProduct(Product product);
        public Product GetProductById(int id);
        public void UpdateProduct(Product product);
        public Product GetProductByName(string name);
        public void DeleteProduct(int id);
        public Product GetUniqueProduct(string name, int supplierId, int categoryId, int sizeId);
    }
}
