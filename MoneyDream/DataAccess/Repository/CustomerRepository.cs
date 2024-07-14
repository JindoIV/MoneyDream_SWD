using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Account> GetListCustomer() => CustomerDAO.Instance.GetListCustomer();
        public void CreateCustomer(Account account) => CustomerDAO.Instance.CreateCustomer(account);
        public Account GetCustomerByID(int id) => CustomerDAO.Instance.GetCustomerByID(id)!;
        public void UpdateCustomer(Account account) => CustomerDAO.Instance.UpdateCustomer(account);
        public Account? GetCustomerByUsername(string? username) => CustomerDAO.Instance.GetCustomerByUsername(username);
        public Account? GetCustomerByPhone(string? phone) => CustomerDAO.Instance.GetCustomerByPhone(phone);
        public Account? GetCustomerByUsernameAndPhone(string? username, string? phone) => CustomerDAO.Instance.GetCustomerByUsernameAndPhone(username, phone);
    }
}
