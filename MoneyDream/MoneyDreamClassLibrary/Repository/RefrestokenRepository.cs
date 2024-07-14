using Microsoft.Identity.Client;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class RefrestokenRepository : IRefreshTokenRepository
    {
        public void Create(int UserID, string token) => RefreshTokenDAO.Instance.Create(UserID, token);


        public RefreshToken GetRefreshTokenByUserID(int UserID) => RefreshTokenDAO.Instance.GetAllRefreshTokenByID(UserID);

    }
}
