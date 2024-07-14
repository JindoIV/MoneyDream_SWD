using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class WishListRepository : IWishListRepository
    {
        public void AddItem(Wishlist item) => WishListDAO.Instance.AddItem(item);

        public IEnumerable<Product> GetAllWishListItem(int AccountID) => WishListDAO.Instance.GetAllWishListItem(AccountID);

        public Product GetWishListItem(int id, int accountID) => WishListDAO.Instance.GetWishListItem(id,accountID);

        public void RemoveItem(int id) => WishListDAO.Instance.RemoveItem(id);
    }
}
