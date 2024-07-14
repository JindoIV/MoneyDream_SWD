namespace MoneyDreamAPI.Dto.PaymentDto
{
    public class CreatePaymentRequest
    {
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
    }
}
