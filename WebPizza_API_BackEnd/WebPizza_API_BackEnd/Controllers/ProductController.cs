using Microsoft.AspNetCore.Mvc;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebPizza_API_BackEnd.ViewModels.Order;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly Cloudinary _cloudinary;

        public ProductController(IProductService productService, ILogger<ProductController> logger, Cloudinary cloudinary)
        {
            _productService = productService;
            _logger = logger;
            _cloudinary = cloudinary;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll([FromQuery] ProductFilterParams parameters)
        {
            var result = await _productService.GetAll(parameters);
            return result;
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetVModel>> GetById(int id)
        {
            var result = await _productService.GetById(id);
            if (result.Value == null)
            {
                return NotFound();
            }
            return result;
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateVModel productDto, IFormFile ImageFile)
        {
            try
            {
                // Kiểm tra trùng tên
                var existingProduct = await _productService.GetProductByNameAsync(productDto.Name);
                if (existingProduct != null)
                {
                    return BadRequest(new { Message = "Product name already exists." });
                }

                // Xử lý upload ảnh
                string imageUrl = null;
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest("Unsupported file type");

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(ImageFile.FileName, ImageFile.OpenReadStream()),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        _logger.LogError("Cloudinary upload error: {ErrorMessage}", uploadResult.Error.Message);
                        return BadRequest($"Cloudinary upload failed: {uploadResult.Error.Message}");
                    }

                    imageUrl = uploadResult.SecureUrl.AbsoluteUri;

                }

                // Gọi service
                var result = await _productService.Create(productDto, imageUrl);

                if (!result.IsSuccess)
                {
                    return BadRequest(result.Message);
                }

                // Ép kiểu để lấy ID
                var successResult = result as SuccessResponseResult;
                return CreatedAtAction(nameof(GetById), new { id = successResult.Id }, new
                {
                    Message = successResult.Message,
                    ProductId = successResult.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating product: {Error}", ex.Message);
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Remove(id);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }

            return Ok(result);
        }
        [HttpGet("{id}/details")]
        public async Task<ActionResult<ProductModel>> GetProductWithDetails(int id)
        {
            try
            {
                var product = await _productService.GetProductWithDetailsAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product details");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}