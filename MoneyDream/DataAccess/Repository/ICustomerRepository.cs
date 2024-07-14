using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        public List<Account> GetListCustomer();
        public void CreateCustomer(Account account);
        public Account GetCustomerByID(int id);
        public void UpdateCustomer(Account account);
        public Account? GetCustomerByUsername(string? username);
        public Account? GetCustomerByPhone(string? phone);
        public Account? GetCustomerByUsernameAndPhone(string? username, string? phone);
    }
}
