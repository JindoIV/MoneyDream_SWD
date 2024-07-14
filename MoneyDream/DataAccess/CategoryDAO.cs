using Microsoft.EntityFrameworkCore;
using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        private CategoryDAO() { }
        private static CategoryDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

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

        public Category? GetCategoryByName(string name)
        {
            Category? category = new Category();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    category = DbContext.Categories.SingleOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get category by name fail!");
            }
            return category;
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

        public void CreateCategory(Category category)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Categories.Add(category);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create category fail!");
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Category>(category).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update category fail!");
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    Category? category = new Category();
                    category = DbContext.Categories.SingleOrDefault(x => x.CategoryId == id);
                    DbContext.Categories.Remove(category!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete category fail!");
            }
        }
    }
}
