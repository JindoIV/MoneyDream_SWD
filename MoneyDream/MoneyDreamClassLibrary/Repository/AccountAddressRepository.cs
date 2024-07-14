using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public   class AccountAddressRepository : IAccountAddressRepository
    {
        public void AddAddress(AccountAddress accountAddress) => AccountAddressDAO.Instance.AddNewAddress(accountAddress);


        public void DeleteAddress(int addressID) => AccountAddressDAO.Instance.DeleteAddress(addressID);

        public AccountAddress GetAddress(int addressID) => AccountAddressDAO.Instance.GetAddresses(addressID);

        public IEnumerable<AccountAddress> GetAllAddress(int accountID) => AccountAddressDAO.Instance.GetAllAddresses (accountID);

        public void UpdateAddress(int addressID,AccountAddress accountAddress) => AccountAddressDAO.Instance.UpdateAddress(addressID,accountAddress) ;
    }
}
