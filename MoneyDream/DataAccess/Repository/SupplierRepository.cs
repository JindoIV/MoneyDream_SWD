using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        public List<Supplier> GetListSupplier() => SupplierDAO.Instance.GetListSupplier();
        public void CreateSupplier(Supplier supplier) => SupplierDAO.Instance.CreateSupplier(supplier);
        public void UpdateSupplier(Supplier supplier) => SupplierDAO.Instance.UpdateSupplier(supplier);
        public Supplier GetSupplierById(int id) => SupplierDAO.Instance.GetSupplierById(id)!;
        public Supplier GetSupplierByName(string name) => SupplierDAO.Instance.GetSupplierByName(name)!;
        public void DeleteSupplier(int id) => SupplierDAO.Instance.DeleteSupplier(id);
    }
}
