using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class PaymentDAO
    {
        private static readonly object InstanceLock = new object();
        private static PaymentDAO instance = null;

        public static PaymentDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PaymentDAO();
                    }
                    return instance;
                }
            }
        }

        public int CreatePayment(Payment payment)
        {
            try
            {
                var context = new MoneyDreamContext();
                var cur = context.Payments.FirstOrDefault(p => p.PaymentId == payment.PaymentId);

                if (cur == null)
                {
                    context.Payments.Add(payment);
                    context.SaveChanges();
                    return payment.PaymentId;
                }
                throw new Exception("Error");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
