using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models ;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetListCategory() => CategoryDAO.Instance.GetListCategory();
        public Category GetCategoryByName(string name) => CategoryDAO.Instance.GetCategoryByName(name)!;
        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id)!;
        public void CreateCategory(Category category) => CategoryDAO.Instance.CreateCategory(category);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.UpdateCategory(category);
        public void DeleteCategory(int id) => CategoryDAO.Instance.DeleteCategory(id);
    }
}
