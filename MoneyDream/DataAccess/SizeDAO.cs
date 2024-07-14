using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models ;

namespace DataAccess
{
    public class SizeDAO
    {
        private SizeDAO() { }
        private static SizeDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static SizeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SizeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Size> GetListSize()
        {
            List<Size> listSize = new List<Size>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listSize = DbContext.Sizes.ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list sizes fail!");
            }
            return listSize;
        }

        public Size? GetSizeByName(string name)
        {
            Size? size = new Size();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    size = DbContext.Sizes.SingleOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get size by name fail!");
            }
            return size;
        }

        public Size? GetSizeById(int id)
        {
            Size? size = new Size();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    size = DbContext.Sizes.SingleOrDefault(x => x.SizeId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get size by id fail!");
            }
            return size;
        }

        public void CreateSize(Size size)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Sizes.Add(size);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create size fail!");
            }
        }

        public void UpdateSize(Size size)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Size>(size).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update size fail!");
            }
        }

        public void DeleteSize(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    Size? size = new Size();
                    size = DbContext.Sizes.SingleOrDefault(x => x.SizeId == id);
                    DbContext.Sizes.Remove(size!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete size fail!");
            }
        }
    }
}
