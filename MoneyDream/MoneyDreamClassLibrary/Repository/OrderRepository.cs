using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public (IEnumerable<object>, int) AdminGetAllOrder(int pageNumber, int pageSize) => OrderDAO.Instance.AdminGetAllOrder(pageNumber, pageSize);

        public object AdminGetOrderByOrderID(int orderID) => OrderDAO.Instance.AdminGetOrderByOrderID((int)orderID);

        public bool CancelOrder(int orderID) => OrderDAO.Instance.CancelOrder(orderID);

        public int CreateOrder(Order order) => OrderDAO.Instance.CreateNewOrder(order);

        public bool DeleteOrder(int orderID) => OrderDAO.Instance.DeleteOrderIfError(orderID);

        public IEnumerable<object> GetAllOrder(int AccountID) => OrderDAO.Instance.GetAllOrder(AccountID);

        public object GetOrder(int orderID) => OrderDAO.Instance.GetOrder(orderID);
    }
}
