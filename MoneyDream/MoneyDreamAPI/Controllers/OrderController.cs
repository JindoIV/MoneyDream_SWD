using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.OrderDto;
using MoneyDreamAPI.Dto.PaginationDto;
using MoneyDreamAPI.Services;
using ProGCoder_MomoAPI.Models.Order;
using WebApi.Authorization;
using WebApi.Services;

namespace MoneyDreamAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/viewOrder")]
        public IActionResult ViewOrder([FromQuery] int orderID)
        {
            try
            {
                var response = _orderService.ViewOrder(orderID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpGet("/viewAllOrder")]
        public IActionResult ViewAllOrder([FromQuery] int accountID)
        {
            try
            {
                var response = _orderService.ViewAllOrder(accountID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpPost("/createOrder")]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            try
            {
                return ApiResponse.Success(_orderService.CreateOrder(request));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [HttpPut("/cancelOrder")]
        public IActionResult CancelOrder(int orderID)
        {
            try
            {
                return ApiResponse.Success(_orderService.CancelOrder(orderID));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }


        [Authorize("ADMIN")]
        [HttpGet("/adminViewAllOrder")]
        public IActionResult AdminViewAllOrder([FromQuery] PaginationRequest parameters)
        {
            try
            {
                var response = _orderService.ViewAllOrderForAdmin(parameters);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }

        [Authorize("ADMIN")]
        [HttpGet("/adminViewOrderByID")]
        public IActionResult AdminViewOrderByID(int OrderID)
        {
            try
            {
                var response = _orderService.ViewOrderByIDForAdmin(OrderID);
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(ex.Message);
            }
        }
    }
}
