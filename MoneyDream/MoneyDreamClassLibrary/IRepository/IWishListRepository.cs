using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IWishListRepository
    {
        public void RemoveItem(int id);
        public void AddItem(Wishlist item);
        public Product GetWishListItem(int id, int accountID);
        public IEnumerable<Product> GetAllWishListItem(int AccountID);
    }
}
