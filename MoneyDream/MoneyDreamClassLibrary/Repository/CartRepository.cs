using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class CartRepository : ICartRepository
    {
        public void AddToCart(Cart item) => CartDAO.Instance.AddToCart(item);

        public void DeleteFromCart(int accountID, int productID, int quantity) => CartDAO.Instance.RemoveFromCart(accountID, productID, quantity);

        public void EditQuantityProductFromCart(int accountID, int productID, int quantity) => CartDAO.Instance.EditQuantityProductFromCart(accountID, productID, quantity);

        public IEnumerable<object> GetAllProductInCart(int accountID) => CartDAO.Instance.GetAllProductInCart(accountID);
    }
}
