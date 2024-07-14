using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.PaymentDto;
using MoneyDreamAPI.Services;
using ProGCoder_MomoAPI.Models.Order;
using WebApi.Services;

namespace MoneyDreamAPI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private IMomoService _momoService;
        private ICustomerService _customerService;

        public PaymentController(IMomoService momoService, ICustomerService customerService)
        {
            _momoService = momoService;
            _customerService = customerService;
        }
        [HttpPost("/testMomo")]
        public async Task<IActionResult> MomoPay(OrderInfoModel model)
        {

            var response = await _momoService.CreatePaymentAsync(model);
            return ApiResponse.Success(response.PayUrl);
        }

        [HttpPost("/createPayment")]
        public IActionResult Pay(CreatePaymentRequest model)
        {

            var response = _customerService.CreatePayment(model);
            return ApiResponse.Success(new {PaymentID=response});
        }
    }
}
