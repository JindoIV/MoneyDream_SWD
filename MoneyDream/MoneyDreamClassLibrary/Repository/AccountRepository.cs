using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class AccountRepository : IAccountRepository 
    {
        public void ActiveAccountByID(int id) => AccountDAO.Instance.ActiveAccountByID(id);
        public void BlockAccountByID(int id) => AccountDAO.Instance.BlockAccountByID(id);
        public Account CreateNewAccount(Account account) => AccountDAO.Instance.CreateNewAccount(account);
        public IEnumerable<Account> GetAllAccount() => AccountDAO.Instance.GetAllAccount();
        public (IEnumerable<Account>, int) GetAllAccount(int pageNumber, int pageSize) => AccountDAO.Instance.GetAllAccount(pageNumber, pageSize);
        public Account GetAllAccountByID(int id) => AccountDAO.Instance.GetAllAccountByID(id);

        public Account GetAllAccountByUsernameOrEmail(string usernameOrEmail) => AccountDAO.Instance.GetAllAccountByUsernameOrEmail(usernameOrEmail);

        public void UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);

        public void UpdatePassword(int id, string password) => AccountDAO.Instance.UpdatePassword(id, password);
    }
}
