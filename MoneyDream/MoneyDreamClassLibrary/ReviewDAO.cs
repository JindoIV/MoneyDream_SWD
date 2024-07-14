using Microsoft.Identity.Client;
using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class ReviewDAO
    {
        private static readonly object InstanceLock = new object();
        private static ReviewDAO instance = null;

        public static ReviewDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReviewDAO();
                    }
                    return instance;
                }
            }
        }
       
        public void CreateReview(Review review)
        {
            Review check;
            try
            {
                var context = new MoneyDreamContext();

               check = context.Reviews.SingleOrDefault(r => r.AccountId == review.AccountId && r.ProductId == review.ProductId);
                if (check != null)
                {
                    throw new Exception("This product has preview by this user!");
                }
                else
                {
                    context.Reviews.Add(review);
                    context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<object> GetProductReview(int productID)
        {
            List<object> result =  new List<object>();
            try
            {
                var context = new MoneyDreamContext();
                var query = (from r in context.Reviews
                             join c in context.Accounts on r.AccountId equals c.AccountId
                             where r.ProductId == productID
                             select new
                             {
                                 User = c,  
                                 Review = r
                             }).ToList();



                foreach (var item in query)
                {
                    result.Add(new
                    {
                        customerName = item.User.FullName,
                        customerAvatar = item.User.Picture,
                        rate = item.Review.Rate,
                        content = item.Review.ReviewContent
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;

        }

    }
}
