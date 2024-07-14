using Microsoft.EntityFrameworkCore;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class CartDAO
    {
        private static readonly object InstanceLock = new object();
        private static CartDAO instance = null;

        public static CartDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<object> GetAllProductInCart(int AccountID)
        {
            List<object> list = new List<object>();
            try
            {

                var context = new MoneyDreamContext();

                var query = (from c in context.Carts
                             join p in context.Products on c.ProductId equals p.ProductId
                             where c.AccountId == AccountID 
                             select new
                             {
                                 quantity = c.Quantity,
                                 p
                             }).ToList();



                if (query != null)  

                    foreach (var item in query)
                    {
                        var size = (from p in context.Products
                                    join s in context.Sizes on p.SizeId equals s.SizeId
                                    where s.SizeId == item.p.SizeId
                                    select new
                                    {
                                        p.ProductId,
                                        s.SizeId,
                                        s.Name,
                                        s.ProductWidth
                                    }).FirstOrDefault();
                        list.Add(new
                        {
                            item.quantity,
                            item.p.ProductId,
                            item.p.Name,
                            item.p.CategoryId,
                            item.p.SupplierId,
                            item.p.ImportId,
                            item.p.OldPrice,
                            item.p.SizeId,
                            item.p.Discount,
                            item.p.Status,
                            item.p.PublicId,
                            item.p.ImageUrlcloud,
                            size

                        });
                    }

                    return list;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }


        public void AddToCart(Cart item)
        {
            Cart check;

            try
            {

                var context = new MoneyDreamContext();
                check = context.Carts.SingleOrDefault(i => 
                i.ProductId == item.ProductId &&
                i.AccountId == item.AccountId);

                if (check == null)
                {
                    context.Carts.Add(item);
                    context.SaveChanges();
                } else
                {
                    check.Quantity += item.Quantity;
                    context.Entry<Cart>(check).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveFromCart(int accountID, int productID, int quantity)
        {
            Cart item;

            try
            {

                var context = new MoneyDreamContext();
                item = context.Carts.SingleOrDefault(i => i.ProductId == productID && i.AccountId == accountID);

                if( item != null )
                {
                    if ( item.Quantity - quantity > 0)
                    {
                        item.Quantity -= quantity;
                        context.Entry<Cart>(item).State = EntityState.Modified;
                    } else
                    {
                        context.Remove(item);
                    }
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EditQuantityProductFromCart(int accountID, int productID, int quantity)
        {
            Cart item;

            try
            {

                var context = new MoneyDreamContext();
                item = context.Carts.SingleOrDefault(i => i.ProductId == productID && i.AccountId == accountID);

                if (item != null)
                {
                    item.Quantity = quantity;
                    context.Entry<Cart>(item).State= EntityState.Modified;
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

