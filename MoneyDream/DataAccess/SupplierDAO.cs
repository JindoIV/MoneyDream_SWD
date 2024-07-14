using BusinessObject.Models ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SupplierDAO
    {
        private SupplierDAO() { }
        private static SupplierDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static SupplierDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SupplierDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Supplier> GetListSupplier()
        {
            List<Supplier> listUnit = new List<Supplier>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listUnit = DbContext.Suppliers.ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list suppliers fail!");
            }
            return listUnit;
        }

        public Supplier? GetSupplierById(int id)
        {
            Supplier? suplier = new Supplier();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    suplier = DbContext.Suppliers.SingleOrDefault(x => x.SupplierId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get supplier by id fail!");
            }
            return suplier;
        }

        public Supplier? GetSupplierByName(string name)
        {
            Supplier? suplier = new Supplier();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    suplier = DbContext.Suppliers.SingleOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get supplier by name fail!");
            }
            return suplier;
        }

        public void CreateSupplier(Supplier supplier)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Suppliers.Add(supplier);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create supplier fail!");
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Supplier>(supplier).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update supplier fail!");
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    Supplier? supplier = DbContext.Suppliers.SingleOrDefault(x => x.SupplierId == id);
                    DbContext.Suppliers.Remove(supplier!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Remove supplier fail!");
            }
        }
    }
}
