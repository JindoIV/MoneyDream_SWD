using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.AccountDto;
using MoneyDreamAPI.Dto.AddressDto;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.CartDto;
using MoneyDreamAPI.Dto.ReviewDto;
using MoneyDreamAPI.Dto.WishlistDto;
using MoneyDreamAPI.Services;
using System.Security.Claims;
using WebApi.Authorization;

namespace MoneyDreamAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService ) {
            _customerService = customerService;
        }


        // Wishlist
        [HttpPost("/addToWishList")]
        [Authorize("CUSTOMER")]
        public IActionResult AddProductToWishList(AddToWishListRequest request)
        {
            try
            {
                _customerService.AddWishListItem(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpDelete("/removeFromWishList")]
        [Authorize("CUSTOMER")]
        public IActionResult RemoveFromWishList(int id)
        {
            try
            {
                _customerService.RemoveWishListItem(id);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        // Cart
        [HttpPost("/addToCart")]
        [Authorize("CUSTOMER")]
        public IActionResult AddProductToCart(AddToCartRequest request)
        {
            try
            {
                _customerService.AddProductToCart(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpGet("/getProductsInCart")]
        [Authorize("CUSTOMER")]
        public IActionResult GetProductsInCart(int accountID)
        {
            try
            {
                var response = _customerService.GetAllProductInCart(accountID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpDelete("/removeFromCart")]
        [Authorize("CUSTOMER")]
        public IActionResult RemoveProductFromCart(RemoveFromCartRequest request)
        {
            try
            {
                _customerService.RemoveFromCart(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpPost("/EditQuantityInCart")]
        [Authorize("CUSTOMER")]
        public IActionResult EditQuantityProductFromCart(RemoveFromCartRequest request)
        {
            try
            {
                _customerService.EditQuantityProductFromCart(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }


        [HttpGet("/profile")]
        [Authorize("CUSTOMER")]
        public IActionResult Profile([FromQuery] int AccountID)
        {
            try
            {
                var response = _customerService.GetProfile(AccountID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpPut("/updateProfile")]
        [Authorize("CUSTOMER")]
        public IActionResult UpdateProfile(int AccountID,UpdateProfileRequest request)
        {
            try
            {
                _customerService.UpdateProfile(AccountID, request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }



        // Review
        [HttpPost("/reviewProduct")]
        [Authorize("CUSTOMER")]
        public IActionResult ReviewProduct(ReviewProductRequest request)
        {
            try
            {
                _customerService.ReviewProduct(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpGet("/getReviewProduct")]
        [Authorize("CUSTOMER")]
        public IActionResult GetProductReview(int productID)
        {
            try
            {
                var response = _customerService.GetReviewProduct(productID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        //Address
        [HttpPost("/createAddress")]
        public IActionResult AddAddress(AddressRequest request)
        {
            try
            {
                _customerService.CreateAddress(request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpPut("/updateAddress")]
        public IActionResult AddAddress(int addressID, AddressRequest request)
        {
            try
            {
                _customerService.UpdateAddress(addressID, request);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpDelete("/deleteAddress")]
        public IActionResult DeleteAddress(int addressID)
        {
            try
            {
                _customerService.DeleteAddress(addressID);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpGet("/getAddress")]
        public IActionResult GetAddress(int addressID)
        {
            try
            {
                var response = _customerService.GetAddress(addressID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpGet("/getAllAddress")]
        public IActionResult GetAllAddAddress(int AccountID)
        {
            try
            {
                var response = _customerService.GetAllAddress(AccountID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }



    }
}
