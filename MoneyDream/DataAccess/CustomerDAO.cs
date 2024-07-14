using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models ;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CustomerDAO
    {
        private CustomerDAO() { }
        private static CustomerDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Account> GetListCustomer()
        {
            List<Account> listAccount = new List<Account>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listAccount = DbContext.Accounts.Include(x => x.Role).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list customers fail!");
            }
            return listAccount;
        }

        public Account? GetCustomerByID(int id)
        {
            Account? account = new Account();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    account = DbContext.Accounts.SingleOrDefault(x => x.AccountId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list customer by id fail!");
            }
            return account;
        }

        public Account? GetCustomerByUsername(string? username)
        {
            Account? account = new Account();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    account = DbContext.Accounts.Include(x => x.Role).SingleOrDefault(x => x.UserName == username);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list customer by username fail!");
            }
            return account;
        }

        public Account? GetCustomerByPhone(string? phone)
        {
            Account? account = new Account();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    account = DbContext.Accounts.Include(x => x.Role).SingleOrDefault(x => x.PhoneNumber == phone);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list customer by phone number fail!");
            }
            return account;
        }

        public Account? GetCustomerByUsernameAndPhone(string? username, string? phone)
        {
            Account? account = new Account();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    account = DbContext.Accounts.Include(x => x.Role).SingleOrDefault(x => x.UserName == username && x.PhoneNumber == phone);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list customer by username and phone fail!");
            }
            return account;
        }

        public void CreateCustomer(Account account)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Accounts.Add(account);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create customer fail!");
            }
        }

        public void UpdateCustomer(Account account)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Account>(account).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update customer fail!");
            }
        }
    }
}
