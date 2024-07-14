namespace MoneyDreamAPI.Dto.AccountDto
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

    }
}
