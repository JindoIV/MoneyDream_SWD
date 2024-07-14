using MoneyDreamClassLibrary.DataAccess;

namespace MoneyDreamAPI.Dto.AuthDto
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        public AuthResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
