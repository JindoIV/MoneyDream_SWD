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
    public class CategoryController : Controller
    {

        private readonly IProductService _productService;

        public CategoryController(IProductService _repository)
        {
            _productService = _repository;
        }
        [HttpGet("/getAllCategory")]
        public  IActionResult GetAllCategory()
        {
            try
            {
                return ApiResponse.Success(_productService.GetAllCategory());
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/getCategoryByID")]
        public IActionResult GetCategoryByID(int categoryID)
        {
            try
            {
                return ApiResponse.Success(_productService.GetCategoryById(categoryID));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

    }
}
