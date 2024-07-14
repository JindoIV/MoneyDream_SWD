using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public int CreatePayment(Payment payment) => PaymentDAO.Instance.CreatePayment(payment);
    }
}
