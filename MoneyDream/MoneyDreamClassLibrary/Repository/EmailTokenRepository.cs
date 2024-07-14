using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class EmailTokenRepository : IEmailTokenRepository
    {
        public object CreateEmailToken(EmailToken token) => EmailTokenDAO.Instance.CreateEmailToken(token);

        public void DeleteEmailToken(int AccountID) => EmailTokenDAO.Instance.DeleteEmailToken(AccountID);

        public EmailToken GetEmailToken(int AccountID) => EmailTokenDAO.Instance.GetEmailToken(AccountID);

        public bool IsValidatedUser(int AccountID) => EmailTokenDAO.Instance.IsValidatedAccount(AccountID);
    }
}
