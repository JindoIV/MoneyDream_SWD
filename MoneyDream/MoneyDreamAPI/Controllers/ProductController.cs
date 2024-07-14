using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using MoneyDreamClassLibrary;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamClassLibrary.Repository;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamAPI.Dto.ProductDto;
using MoneyDreamAPI.Dto.PaginationDto;
using WebApi.Authorization;


namespace MoneyDreamAPI.Controllers
{

    [ApiController]
    [Route("/products")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService _repository)
        {
            _productService = _repository;
        }

        // Retrieves all products, optionally paginated by pageId.
        [HttpGet()]
        public IActionResult ViewHomePage([FromQuery] PaginationRequest request)
        {
            try
            {
                return ApiResponse.Success(_productService.GetAllProductCatalog(request));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/product")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var response = _productService.GetProductByID(id);
                if (response == null)
                {
                    return ApiResponse.RequestError("Not found the product");
                }
                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/getProductsByCategoryID")]
        public IActionResult GetProductByCategoryId(int categoryID)
        {
            try
            {
                return ApiResponse.Success(_productService.GetProductByCategoryID(categoryID));
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }

        [HttpGet("/getSupplier")]
        public IActionResult GetAllSupplier()
        {
            try
            {
                return ApiResponse.Success(_productService.GetAllSuppliers());
            }
            catch (Exception ex)
            {
                return ApiResponse.Error(null, ex.Message);
            }
        }


    }
}
