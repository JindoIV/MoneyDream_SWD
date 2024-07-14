using Microsoft.AspNetCore.Mvc;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using WebApi.Services;
using MoneyDreamClassLibrary.DataAccess;
using WebApi.Authorization;
using CloudinaryDotNet;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.PaginationDto;

namespace MoneyDreamAPI.Controllers
{

    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpGet("/")]
        [Authorize("ADMIN")]
        public IActionResult GetAll([FromQuery] PaginationRequest parameters)
        {
            try
            {
                return ApiResponse.Success(accountService.GetAllAccount(parameters));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }


        [HttpGet("/id")]
        [Authorize("ADMIN")]
        public IActionResult GetAccount(int id)
        {
            try
            {
                return ApiResponse.Success(accountService.GetAccountByID(id));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/active")]
        [Authorize("ADMIN")]
        public IActionResult ActiveAccount(int id)
        {
            try
            {
                accountService.ActiveAccount(id);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/block")]
        [Authorize("ADMIN")]
        public IActionResult BlockAccount(int id)
        {
            try
            {
                accountService.BlockAccount(id);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

    }
}
