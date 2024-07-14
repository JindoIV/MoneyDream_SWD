using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IAccountRepository
    {
        (IEnumerable<Account>, int)  GetAllAccount(int pageNumber, int pageSize);
        IEnumerable<Account> GetAllAccount();
        Account GetAllAccountByID(int id);
        Account GetAllAccountByUsernameOrEmail(string usernameOrEmail);
        void BlockAccountByID(int id);
        void ActiveAccountByID(int id);
        void UpdatePassword(int id, string password);
        Account CreateNewAccount(Account account);
        void UpdateAccount(Account account);
    }
}
