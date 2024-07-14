namespace MoneyDreamAPI.Dto.AuthDto
{
    public class ChangePasswordRequest
    {
        public int AccountID { get; set; }
        public string NewPassword { get; set; }
    }
}
