using Microsoft.EntityFrameworkCore;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class AccountDAO
    {
        private static readonly object InstanceLock = new object();
        private static AccountDAO instance = null;

        public static AccountDAO Instance
        {
            get
            {
                lock (InstanceLock) { 
                    if(instance == null)
                    {
                        instance = new AccountDAO();    
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Account> GetAllAccount()
        {
            List<Account> accounts;
            try
            {

                var context = new MoneyDreamContext();
                accounts = context.Accounts.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return accounts;
        }

        public (IEnumerable<Account>,int) GetAllAccount(int pageNumber, int pageSize)
        {
            List<Account> accounts;
            int totalRecord = 0;
            try
            {

                var context = new MoneyDreamContext();
                accounts = context.Accounts.OrderBy(on => on.FullName)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
                totalRecord = context.Accounts.Count();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return (accounts, totalRecord);
        }

        public Account GetAllAccountByID(int id)
        {
            Account account;

            try
            {

                var context = new MoneyDreamContext();
                account = context.Accounts.SingleOrDefault(acc => acc.AccountId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account GetAllAccountByUsernameOrEmail(string query)
        {
            Account account;

            try
            {

                var context = new MoneyDreamContext();
                account = context.Accounts.SingleOrDefault(acc => acc.UserName == query || acc.Email == query);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account CreateNewAccount(Account account)
        {
            try
            {
                var context = new MoneyDreamContext();
                var checkAccount = context.Accounts.SingleOrDefault(acc =>acc.UserName == account.UserName);
                if (checkAccount != null) throw new Exception("Username is early exist!");
                checkAccount = context.Accounts.SingleOrDefault(acc => acc.Email == account.Email);
                if (checkAccount != null) throw new Exception("Email is early exist!");
                checkAccount = context.Accounts.SingleOrDefault(acc => acc.PhoneNumber == account.PhoneNumber);
                if (checkAccount != null) throw new Exception("PhoneNumber is early exist!");

                context.Accounts.Add(account);
                context.SaveChanges();


                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void BlockAccountByID(int id)
        {
            Account account;

            try
            {

                var context = new MoneyDreamContext();
                account = context.Accounts.SingleOrDefault(acc => acc.AccountId == id);
                if (account.Status == "ACTIVE")
                {
                    account.Status = "BLOCKED";
                }

                context.Entry<Account>(account).State = EntityState.Modified;
                context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActiveAccountByID(int id)
        {
            Account account;

            try
            {

                var context = new MoneyDreamContext();
                account = context.Accounts.SingleOrDefault(acc => acc.AccountId == id);
                if (account.Status == "BLOCKED")
                {
                    account.Status = "ACTIVE";
                }

                context.Entry<Account>(account).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePassword(int id, string password)
        {
            Account account;

            try
            {

                var context = new MoneyDreamContext();
                account = context.Accounts.SingleOrDefault(acc => acc.AccountId == id);
                if(account != null)
                    account.Password = password;
                else 
                    throw new Exception("Account not exist!");

                context.Entry<Account>(account).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateAccount(Account account)
        {

            try
            {

                var context = new MoneyDreamContext();
                var cur = context.Accounts.SingleOrDefault(acc => acc.AccountId == account.AccountId);
                if (cur != null)
                {
                    cur.FullName = account.FullName;
                    cur.Age = account.Age;
                    cur.Gender= account.Gender;
                    cur.DateofBirth = account.DateofBirth;
                    cur.PhoneNumber = account.PhoneNumber;
                    cur.Picture = account.Picture;  

                    context.Entry<Account>(cur).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
