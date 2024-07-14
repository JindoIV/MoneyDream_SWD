using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public bool CreateNewOrderDetails(List<OrderDetail> items) => OrderDetailDAO.Instance.CreateNewOrderDetails(items);
    }
}
