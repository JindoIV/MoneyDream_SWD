using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IReviewRepository
    {
        public void CreateReview(Review review);

        public IEnumerable<object> GetProductReview(int productID);

    }
}
