using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.AuthDto;
using MoneyDreamAPI.Services;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using WebApi.Services;

namespace MoneyDreamAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }


        [HttpPost("/")]
        public IActionResult Auth(string token)
        {
            try
            {
                var response = authService.Authentication(token);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/refreshToken")]
        public IActionResult RefreshToken(string token)
        {
            try
            {
                var response = authService.RefreshToken(token);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/login")]
        public IActionResult SignIn(AuthRequest model)
        {
            try
            {
                var response = authService.Login(model);

                if (response == null)
                    return ApiResponse.RequestError(null, "Username or password is incorrect");

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }


        [HttpPost("/register")]
        public IActionResult Register(RegisterRequest model)
        {
            try
            {
                var response = authService.Register(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/validateEmail")]
        public IActionResult ValidateEmail(ValidateEmailRequest model)
        {
            try
            {
                var response = authService.ValidateEmail(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/ReValidateEmail")]
        public IActionResult ReValidateEmail(ReSendValidateEmail model)
        {
            try
            {
                var response = authService.ReSendValidateEmail(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/sendRecoverPasswordEmail")]
        public IActionResult SendRecoverPasswordEmail(RecoverPasswordRequest model)
        {
            try
            {
                var response = authService.SendRecoverPasswordEmail(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/reSendRecoverPasswordEmail")]
        public IActionResult ReSendRecoverPasswordEmail(RecoverPasswordRequest model)
        {
            try
            {
                var response = authService.ReSendRecoverPasswordEmail(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpPost("/changePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest model)
        {
            try
            {
                var response = authService.ChangePassword(model);

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }
    }
}
