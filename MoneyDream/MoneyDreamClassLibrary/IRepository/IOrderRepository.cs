using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IOrderRepository
    {
        public int CreateOrder(Order order);
        public bool DeleteOrder(int orderID);
        public bool CancelOrder(int orderID);
        public object GetOrder(int orderID);
        public IEnumerable<object> GetAllOrder(int AccountID);
        public (IEnumerable<object>, int) AdminGetAllOrder(int pageNumber, int pageSize);
        public object AdminGetOrderByOrderID(int orderID);
    }
}
