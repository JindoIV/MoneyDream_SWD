using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface ICartRepository
    {
        public IEnumerable<object> GetAllProductInCart(int accountID);

        public void AddToCart(Cart item);

        public void DeleteFromCart(int accountID, int productID, int quantity);
        public void EditQuantityProductFromCart(int accountID, int productID, int quantity);
    }
}
