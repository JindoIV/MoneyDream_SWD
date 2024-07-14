using Microsoft.EntityFrameworkCore;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class WishListDAO
    {
        private static readonly object InstanceLock = new object();
        private static WishListDAO instance = null;

        public static WishListDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WishListDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetAllWishListItem(int AccountID)
        {
            List<Product> list;
            try
            {

                var context = new MoneyDreamContext();

                list = (from w in context.Wishlists
                             join p in context.Products on w.ProductId equals p.ProductId
                             where w.AccountId == AccountID
                             select p).ToList();

                if ( list != null) 
                    return list;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Product GetWishListItem(int id, int accountID)
        {
            try
            {

                var context = new MoneyDreamContext();
                var query = (from w in context.Wishlists
                             join p in context.Products on w.ProductId equals p.ProductId
                             where (w.WishlistId == id && w.AccountId == accountID)
                             select p).Take(1);

                if( query.Any() )
                {
                    return query.First();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public void AddItem(Wishlist item)
        {
            Wishlist check;

            try
            {

                var context = new MoneyDreamContext();
                check = context.Wishlists.SingleOrDefault(i => 
                i.ProductId == item.ProductId &&
                i.AccountId == item.AccountId);

                if (check == null)
                {
                    context.Wishlists.Add(item);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveItem(int id)
        {
            Wishlist item;

            try
            {

                var context = new MoneyDreamContext();
                item = context.Wishlists.SingleOrDefault(i => i.WishlistId == id);

                if( item != null )
                {
                    context.Wishlists.Remove(item);
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

