namespace MoneyDreamAPI.Dto.AuthDto
{
    public class RegisterResponse
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DateofBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
