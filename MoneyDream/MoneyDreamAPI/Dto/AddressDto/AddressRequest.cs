namespace MoneyDreamAPI.Dto.AddressDto
{
    public class AddressRequest
    {
        public int AccountID { get; set; }  
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }      
    }
}
