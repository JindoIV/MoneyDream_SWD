namespace MoneyDreamAPI.Dto.OrderDto
{
    public class OrderProduct
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }



    public class CreateOrderRequest
    {
        public int AccountID { get; set; }
        public int AddressID { get; set; }
        public int PaymentID { get; set; }
        public int? VoucherID { get; set; }
        public OrderProduct[] OrderProducts { get; set; }
    }
}
