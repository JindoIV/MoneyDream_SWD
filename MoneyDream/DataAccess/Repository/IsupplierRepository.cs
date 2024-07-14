using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISupplierRepository
    {
        public List<Supplier> GetListSupplier();
        public void CreateSupplier(Supplier supplier);
        public void UpdateSupplier(Supplier supplier);
        public Supplier GetSupplierById(int id);
        public Supplier GetSupplierByName(string name);
        public void DeleteSupplier(int id);
    }
}
