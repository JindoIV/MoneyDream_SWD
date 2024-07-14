using Microsoft.Identity.Client;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class EmailTokenDAO
    {
        private static readonly object InstanceLock = new object();
        private static EmailTokenDAO instance = null;

        public static EmailTokenDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmailTokenDAO();
                    }
                    return instance;
                }
            }
        }

        public object CreateEmailToken(EmailToken token)
        {
            try
            {
                var context = new MoneyDreamContext();
                var existToken = context.EmailTokens.FirstOrDefault(x => x.AccountId == token.AccountId);

                if (existToken != null)
                {
                    context.Remove(existToken);
                }  

                context.Add(token);
                context.SaveChanges();
                var tokenObject = new
                {
                    Token = token?.Token,
                    CreatedAt = token?.Created
                };
                return tokenObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public EmailToken GetEmailToken(int AccountID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var token = context.EmailTokens.SingleOrDefault(r => r.AccountId == AccountID);

                if (token != null)

                    return token;

                else
                    throw new Exception("Email token is not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsValidatedAccount(int accountID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var token = context.EmailTokens.SingleOrDefault(r => r.AccountId == accountID);
                return token== null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteEmailToken(int accountID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var token = context.EmailTokens.SingleOrDefault(r => r.AccountId == accountID);
                context.Remove(token);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
