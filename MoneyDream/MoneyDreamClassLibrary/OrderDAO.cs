using Azure;
using Microsoft.EntityFrameworkCore;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class OrderDAO
    {
        private static readonly object InstanceLock = new object();
        private static OrderDAO instance = null;

        public static OrderDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public object GetOrder(int orderId)
        {
            
            try
            {
                var context = new MoneyDreamContext();
                var order = context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                var address = context.AccountAddresses.SingleOrDefault(o => o.AddressId == order.AddressId);
                var account = context.Accounts.SingleOrDefault(o => o.AccountId == order.AccountId);
                var status = context.OrderStatuses.SingleOrDefault(o => o.StatusId == order.OrderStatusId);
                var od = context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
                var product = new List<object>();

                foreach (var item in od)
                {
                    var pro = context.Products.SingleOrDefault(p => p.ProductId == item.ProductId);
                    var proImgs = context.ProductImages.Where(p => p.ProductId == pro.ProductId).ToList();
                    var proSize = context.Sizes.Where(p => p.SizeId == pro.SizeId).FirstOrDefault();
                    product.Add(new
                    {
                        pro.ProductId,
                        pro.Name,
                        item.Quantity,
                        proSize,
                        images = proImgs,
                    });
                }
                return new {
                    order = new
                    {
                        orderId = orderId,
                        status = status.StatusName,
                        customer = new {
                            customerId = account.AccountId,  
                            customerName = account.FullName,
                        },
                        address = new
                        {
                            addressd = address.AddressId,
                            name = address.DeliveryName,
                            phone = address.DeliveryPhone,
                            address = address.Address,
                        },
                        product = product
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public IEnumerable<object> GetAllOrder(int customerID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var result = context.Orders.Where(o => o.AccountId == customerID).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public (IEnumerable<object>, int) AdminGetAllOrder(int pageNumber, int pageSize) { 
            var response =  new List<object>();
            int totalRecord = 0;
            try
            {
                var context = new MoneyDreamContext();
                var orders = (from O in context.Orders select new
                {
                    O.OrderId,
                    O.TotalAmount,
                    O.OrderDate,
                    O.UpdateStatusAt,
                    O.OrderStatusId,
                    O.AddressId,
                    O.AccountId,
                    O.PaymentId
                })
                    .OrderBy(on => on.OrderId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                totalRecord = context.Orders.Count();

                if(orders != null)
                foreach (var order in orders)
                {
                    var account  = (from A in context.Accounts
                                    join O in context.Orders
                                    on A.AccountId equals O.AccountId
                                    where O.OrderId == order.OrderId
                                    select new
                                    {
                                        A.AccountId,
                                        A.FullName,
                                        A.Picture,
                                    } ).SingleOrDefault(a => a.AccountId == order.AccountId);

                    var orderStatus = (from S in context.OrderStatuses
                                       join O in context.Orders
                                       on S.StatusId equals O.OrderStatusId
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           O.OrderId,
                                           S.StatusId,
                                           S.StatusName,
                                           S.StatusCode,
                                       }).SingleOrDefault(a => a.OrderId == order.OrderId);

                        var address = (from O in context.Orders
                                       join Ad in context.AccountAddresses on O.AddressId equals Ad.AddressId 
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           O.OrderId,
                                           Ad.Address,
                                           Ad.DeliveryName,
                                           Ad.DeliveryPhone
                                       }
                                       ).SingleOrDefault(x => x.OrderId == order.OrderId);
                        var payment = (from O in context.Orders
                                       join P in context.Payments on O.PaymentId equals P.PaymentId
                                       join PT in context.PaymentTypes on P.Type equals PT.PayTypeId
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           O.OrderId,
                                           P.Amount,
                                           PT.TypeName,
                                       }).SingleOrDefault(x => x.OrderId == order.OrderId);

                    response.Add(new
                    {
                        Order = order,
                        Account = account  ,
                        OrderStatus = orderStatus,
                        Address = address,
                        Payment = payment
                    });
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (response,totalRecord);
        }

        public object AdminGetOrderByOrderID(int orderID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var order = (from O in context.Orders
                              select new
                              {
                                  O.OrderId,
                                  O.TotalAmount,
                                  O.OrderDate,
                                  O.UpdateStatusAt,
                                  O.OrderStatusId,
                                  O.AddressId,
                                  O.AccountId,
                                  O.PaymentId
                              }).SingleOrDefault(x => x.OrderId == orderID);

                if (order != null)
                    {
                        var account = (from A in context.Accounts
                                       join O in context.Orders
                                       on A.AccountId equals O.AccountId
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           A.AccountId,
                                           A.FullName,
                                           A.Picture,
                                       }).SingleOrDefault(a => a.AccountId == order.AccountId);

                        var orderStatus = (from S in context.OrderStatuses
                                           join O in context.Orders
                                           on S.StatusId equals O.OrderStatusId
                                           where O.OrderId == order.OrderId
                                           select new
                                           {
                                               O.OrderId,
                                               S.StatusId,
                                               S.StatusName,
                                               S.StatusCode,
                                           }).SingleOrDefault(a => a.OrderId == order.OrderId);

                        var address = (from O in context.Orders
                                       join Ad in context.AccountAddresses on O.AddressId equals Ad.AddressId
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           O.OrderId,
                                           Ad.Address,
                                           Ad.DeliveryName,
                                           Ad.DeliveryPhone
                                       }
                                       ).SingleOrDefault(x => x.OrderId == order.OrderId);
                        var payment = (from O in context.Orders
                                       join P in context.Payments on O.PaymentId equals P.PaymentId
                                       join PT in context.PaymentTypes on P.Type equals PT.PayTypeId
                                       where O.OrderId == order.OrderId
                                       select new
                                       {
                                           O.OrderId,
                                           P.Amount,
                                           PT.TypeName,
                                       }).SingleOrDefault(x => x.OrderId == order.OrderId);



                        return (new
                        {
                            Order = order,
                            Account = account,
                            OrderStatus = orderStatus,
                            Address = address,
                            Payment = payment
                        });
                    }
            } catch (Exception ex) { 
            throw new Exception(ex.Message, ex);
            }
            return null;
        }

        public int CreateNewOrder(Order newOrder)
        {
            try
            {
                var context = new MoneyDreamContext();
                var order = context.Orders.SingleOrDefault(o => o.OrderId == newOrder.OrderId);

                if (order == null)
                {
                    context.Add(newOrder);
                    context.SaveChanges();
                }

                return newOrder.OrderId;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public bool DeleteOrderIfError(int orderID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var order = context.Orders.SingleOrDefault(o => o.OrderId == orderID);

                if (order != null)
                {
                    context.Orders.Remove(order);
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

        public bool CancelOrder(int orderID)
        {
            try
            {
                var context = new MoneyDreamContext();
                var order = context.Orders.SingleOrDefault(o => o.OrderId == orderID);

                if (order != null)
                {
                    order.OrderStatusId = 4;

                    context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

