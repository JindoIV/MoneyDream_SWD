using Microsoft.AspNetCore.Mvc;
using MoneyDreamAPI.Dto.ApiResponse;
using MoneyDreamAPI.Dto.ImageExtension;
namespace MoneyDreamAPI

{
    [ApiController]
    [Route("api/")]

    //This is an example class for uploading and get URL image
    public class ImageController : Controller
    {

        [HttpPost("/delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                var a = ImageExtension.Delete(id);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpPost("/edit")]
        public IActionResult Edit(string id, IFormFile file)
        {
            try
            {
                var a = ImageExtension.Edit(id, file);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }

        [HttpPost("/upload")]
        public IActionResult UploadMultiple(List<IFormFile> file)
        {
            try
            {
                var a = ImageExtension.UploadMultiple(file);
                return ApiResponse.Success(a);
            }

            catch (Exception e)
            {
                return ApiResponse.Error(null, e.Message);
            }
        }
    }
}
