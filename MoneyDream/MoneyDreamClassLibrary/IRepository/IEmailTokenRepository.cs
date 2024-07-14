using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IEmailTokenRepository
    {
        public bool IsValidatedUser(int AccountID);

        public EmailToken GetEmailToken(int AccountID);

        public object CreateEmailToken(EmailToken token);

        public void DeleteEmailToken(int AccountID);
    }
}
