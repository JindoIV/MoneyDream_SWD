using Microsoft.AspNetCore.Identity;
using MoneyDreamAPI.Dto.AuthDto;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using System.Diagnostics.Eventing.Reader;
using WebApi.Authorization;

namespace MoneyDreamAPI.Services
{

    public interface IAuthService
    {
        AuthResponse? Login(AuthRequest model);
        MoneyDreamClassLibrary.DataAccess.Account Register(RegisterRequest model);
        MoneyDreamClassLibrary.DataAccess.Account? Authentication(string token);
        AuthResponse? RefreshToken(string token);
        bool ValidateEmail(ValidateEmailRequest model);
        bool ReSendValidateEmail(ReSendValidateEmail model);
        bool SendRecoverPasswordEmail(RecoverPasswordRequest model);
        bool ReSendRecoverPasswordEmail(RecoverPasswordRequest model);

        string SendRole(string role);
        bool ChangePassword(ChangePasswordRequest model);

    }
    public class AuthService : IAuthService
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IAccountRepository repository;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailTokenRepository _emailTokenRepository;

        public AuthService(IJwtUtils jwtUtils, IEmailService emailService)
        {
            _jwtUtils = jwtUtils;
            _emailService = emailService;
            _emailTokenRepository = new EmailTokenRepository();
            repository = new AccountRepository();
            refreshTokenRepository = new RefrestokenRepository();
        }

        public AuthResponse? Login(AuthRequest model)
        {
            var users = repository.GetAllAccount();
            MoneyDreamClassLibrary.DataAccess.Account user = users.SingleOrDefault(x => x.UserName == model.Username && CheckHashed(model.Password, x.Password));

            // return null if user not found
            if (user == null) return null;

            if (!CheckIsValidatedEmail(user.AccountId)) throw new Exception("Email is not validated");

            // authentication successful so generate jwt token
            var accessToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateJwtRefreshToken(accessToken);

            refreshTokenRepository.Create(user.AccountId, refreshToken);

            return new AuthResponse(accessToken, refreshToken);
        }
        public AuthResponse? RefreshToken(string token)
        {
            var refreshToken = _jwtUtils.GenerateJwtRefreshToken(token);

            return new AuthResponse(token, refreshToken);
        }

        public MoneyDreamClassLibrary.DataAccess.Account? Authentication(string token)
        {
            var validatedUser = _jwtUtils.ValidateJwtToken(token);

            return validatedUser != null ? this.repository.GetAllAccountByID((int)validatedUser) : null;
        }

        public MoneyDreamClassLibrary.DataAccess.Account Register(RegisterRequest model)
        {
            var account = new MoneyDreamClassLibrary.DataAccess.Account();
            account.FullName = model.FullName;
            account.UserName = model.UserName;
            account.Password = Hashing(model.Password);
            account.Email = model.Email;
            account.PhoneNumber = model.PhoneNumber;
            account.DateofBirth = model.DateofBirth;
            account.Created = DateTime.Now.ToString();
            account.RoleId = 2;

            repository.CreateNewAccount(account);

            var _emailToken = new EmailToken();
            _emailToken.Token = RandomValidateCode();
            _emailToken.Created = DateTime.Now.ToString();
            _emailToken.AccountId = account.AccountId;

            _emailTokenRepository.CreateEmailToken(_emailToken);

            _emailService.Send(account.Email, "REGISTER ACCOUNT", "<b>Hi<b>", null);

            return account;
        }

        public bool ReSendValidateEmail(ReSendValidateEmail model)
        {
            var _emailToken = new EmailToken();
            _emailToken.Token = RandomValidateCode();
            _emailToken.Created = DateTime.Now.ToString();
            _emailToken.AccountId = model.AccountID;

            _emailTokenRepository.CreateEmailToken(_emailToken);

            _emailService.Send(model.Email, "RE-REGISTER ACCOUNT", "<b>Hi<b>", null);
            return true;
        }

        public bool SendRecoverPasswordEmail(RecoverPasswordRequest model)
        {
            try
            {
                var account = repository.GetAllAccountByUsernameOrEmail(model.UsernameOrEmail);

                var _emailToken = new EmailToken();
                _emailToken.Token = RandomValidateCode();
                _emailToken.Created = DateTime.Now.ToString();
                _emailToken.AccountId = account.AccountId;

                _emailTokenRepository.CreateEmailToken(_emailToken);

                _emailService.Send(account.Email, "RECOVER PASSWORD", "<b>Hi<b>", null);
            } catch (Exception e)
            {
                return false;
            }
            return true;

        }

        public bool ReSendRecoverPasswordEmail(RecoverPasswordRequest model)
        {
            try
            {
                var account = repository.GetAllAccountByUsernameOrEmail(model.UsernameOrEmail);

                var _emailToken = new EmailToken();
                _emailToken.Token = RandomValidateCode();
                _emailToken.Created = DateTime.Now.ToString();
                _emailToken.AccountId = account.AccountId;

                _emailTokenRepository.CreateEmailToken(_emailToken);

                _emailService.Send(account.Email, "RE-RECOVER PASSWORD", "<b>Hi<b>", null);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private string Hashing(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password,13);
        }

        private bool CheckHashed(string origin, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(origin, hash);
        }

        private bool CheckIsValidatedEmail(int AccountID)
        {
            return _emailTokenRepository.IsValidatedUser(AccountID);
        }

        private string RandomValidateCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }

        private bool CheckValidatedCode(int AccountID, string code)
        {

            var dbToken = _emailTokenRepository.GetEmailToken(AccountID);
            var dbCode = dbToken.Token;
            var exp = dbToken.Created;

            if (dbCode == code)
            {
                var elapsedTime = DateTime.Now - DateTime.Parse(exp);

                if (elapsedTime.TotalMinutes >= 10)
                {
                    throw new Exception("Expired code");
                }
            }
            else
            {
                throw new Exception("Invalid code");
            }

            return true;
        }

        public bool ChangePassword(ChangePasswordRequest model)
        {
            repository.UpdatePassword(model.AccountID,Hashing(model.NewPassword));
            return true;
        }

        public bool ValidateEmail(ValidateEmailRequest model)
        {
            var check = CheckValidatedCode(model.AccountID, model.Code);
            if (check)
            {
                _emailTokenRepository.DeleteEmailToken(model.AccountID);    
                return true;
            }
            return false;
        }

        public string SendRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
