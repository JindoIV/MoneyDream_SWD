namespace MoneyDreamAPI.Dto.ReviewDto
{
    public class ReviewProductRequest
    {
        public int AcccountID { get; set; }
         
        public int ProductID { get; set; }

        public int ratePoint { get; set; }

        public string? content { get; set; }
    }   
}
