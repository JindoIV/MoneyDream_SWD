using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        public List<Category> GetListCategory();
        public Category GetCategoryByName(string name);
        public Category GetCategoryById(int id);
        public void CreateCategory(Category category);
        public void UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}
