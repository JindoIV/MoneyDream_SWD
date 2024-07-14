using Microsoft.EntityFrameworkCore;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class RefreshTokenDAO
    {
        private static readonly object InstanceLock = new object();
        private static RefreshTokenDAO instance = null;

        public static RefreshTokenDAO Instance
        {
            get
            {
                lock (InstanceLock) { 
                    if(instance == null)
                    {
                        instance = new RefreshTokenDAO();    
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<RefreshToken> GetAllRefreshToken()
        {
            List<RefreshToken> RefreshTokens;

            try
            {

                var context = new MoneyDreamContext();
                RefreshTokens = context.RefreshTokens.ToList();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RefreshTokens;
        }

        public RefreshToken GetAllRefreshTokenByID(int id)
        {
            RefreshToken RefreshToken;

            try
            {

                var context = new MoneyDreamContext();
                RefreshToken = context.RefreshTokens.SingleOrDefault(acc => acc.RefreshTokenId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RefreshToken;
        }

        public RefreshToken GetAllRefreshTokenByUserID(int UserID)
        {
            RefreshToken RefreshToken;

            try
            {

                var context = new MoneyDreamContext();
                RefreshToken = context.RefreshTokens.SingleOrDefault(acc => acc.AccountId == UserID);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RefreshToken;
        }

        public void Create(int UserID, string token)
        {
            RefreshToken _RefreshToken;

            try
            {
                var context = new MoneyDreamContext();

                RefreshToken currentToken = context.RefreshTokens.SingleOrDefault(token => token.AccountId == UserID);
                if(currentToken != null)
                {
                    context.RefreshTokens.Remove(currentToken);
                    _RefreshToken = new RefreshToken() { Token = token, AccountId = UserID, Created = DateTime.Now.ToString()  };
                    context.RefreshTokens.Add(_RefreshToken);
                    context.SaveChanges();
                } 
                else
                {
                    _RefreshToken = new RefreshToken() { Token = token, AccountId = UserID, Created = DateTime.Now.ToString() };
                    context.RefreshTokens.Add(_RefreshToken);
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
