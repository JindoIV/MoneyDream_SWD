namespace MoneyDreamAPI.Dto.CartDto
{
    public class AddToCartRequest
    {
        public int AcccountID { get; set; }
        public int ProductID { get; set;}
        public int Quantity { get; set; }
    }
}
