using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        public void CreateReview(Review review) => ReviewDAO.Instance.CreateReview(review);

        public IEnumerable<object> GetProductReview(int productID) => ReviewDAO.Instance.GetProductReview(productID);
         
    }
}
