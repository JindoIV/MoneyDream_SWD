using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class OrderDetailDAO
    {
        private static readonly object InstanceLock = new object();
        private static OrderDetailDAO instance = null;

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }




        public bool CreateNewOrderDetails(List<OrderDetail> newItem)
        {
            try
            {
                var context = new MoneyDreamContext();
                foreach (var item in newItem)
                {
                    var order = context.OrderDetails.SingleOrDefault(o => o.OrderDetailsId == item.OrderDetailsId);

                    if (order == null)
                    {
                        context.OrderDetails.Add(item);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteOrderIfError(int itemID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var order = context.OrderDetails.SingleOrDefault(o => o.OrderDetailsId == itemID);

                if (order != null)
                {
                    context.OrderDetails.Remove(order);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}