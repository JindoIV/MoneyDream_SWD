using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IAccountAddressRepository
    {
        public AccountAddress GetAddress(int addressID);
        public IEnumerable<AccountAddress> GetAllAddress(int accountID);
        public void AddAddress(AccountAddress accountAddress);
        public void UpdateAddress(int addressID, AccountAddress accountAddress);
        public void DeleteAddress(int addressID);
    }
}
