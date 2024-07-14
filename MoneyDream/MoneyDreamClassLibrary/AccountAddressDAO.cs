using Microsoft.Identity.Client;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class AccountAddressDAO
    {
        private static readonly object InstanceLock = new object();
        private static AccountAddressDAO instance = null;

        public static AccountAddressDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountAddressDAO();
                    }
                    return instance;
                }
            }
        }

        public void AddNewAddress(AccountAddress address)
        {
            try
            {
                var context = new MoneyDreamContext();
                AccountAddress ad = context.AccountAddresses.SingleOrDefault(a => a.AddressId == address.AddressId);
                if(ad == null )
                {
                    context.Add(address);
                    context.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AccountAddress> GetAllAddresses(int accountID) {
            IEnumerable<AccountAddress> currentAddress;
            try
            {
                var context = new MoneyDreamContext();
                currentAddress = context.AccountAddresses.ToList().Where(address => address.AccountId == accountID && address.DeleteAt == null);   
                if (currentAddress.Any())
                {
                    return currentAddress;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return currentAddress;
        }

        public AccountAddress GetAddresses(int addressID)
        {
            AccountAddress cur;
            try
            {
                var context = new MoneyDreamContext();
                cur = context.AccountAddresses.SingleOrDefault(address => address.AddressId == addressID && address.DeleteAt==null   );
                if (cur != null)
                {
                    return cur;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cur;
        }

        public void UpdateAddress(int addressID, AccountAddress address)
        {
            AccountAddress cur;
            try
            {
                var context = new MoneyDreamContext();
                cur = context.AccountAddresses.SingleOrDefault(a => a.AddressId == addressID);
                if (cur != null)
                {
                    cur.Address = address.Address;
                    cur.DeliveryPhone = address.DeliveryPhone;  
                    cur.DeliveryName = address.DeliveryName;
                    context.Entry<AccountAddress>(cur).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAddress(int addressID)
        {
            AccountAddress cur;
            try
            {
                var context = new MoneyDreamContext();
                cur = context.AccountAddresses.SingleOrDefault(address => address.AddressId == addressID);
                if (cur != null)
                {
                    cur.DeleteAt = DateTime.Now.ToString();
                    context.Entry<AccountAddress>(cur).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
