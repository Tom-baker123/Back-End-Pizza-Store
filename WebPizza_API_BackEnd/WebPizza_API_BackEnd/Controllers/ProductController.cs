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


        [HttpGet]
        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll([FromQuery] ProductFilterParams parameters)
        {
            var result = await _productService.GetAll(parameters);
            return result;
        }


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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateVModel productDto, IFormFile ImageFile)
        {
            try
            {
                // Kiểm tra sản phẩm có tồn tại hay không
                var existingProduct = await _productService.GetById(id);
                if (existingProduct == null)
                {
                    return NotFound(new { Message = "Product not found." });
                }

                // Xử lý upload ảnh nếu có ảnh mới
                string imageUrl = existingProduct.Value?.ImageURL; // Giữ ảnh cũ nếu không có ảnh mới
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest(new { Message = "Unsupported file type" });

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(ImageFile.FileName, ImageFile.OpenReadStream()),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        _logger.LogError("Cloudinary upload error: {ErrorMessage}", uploadResult.Error.Message);
                        return BadRequest(new { Message = $"Cloudinary upload failed: {uploadResult.Error.Message}" });
                    }

                    imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                }

                // Gọi service để cập nhật sản phẩm
                productDto.ImageURL = imageUrl; // Cập nhật URL ảnh vào model
                var result = await _productService.Update(id, productDto);

                if (!result.IsSuccess)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new { Message = "Product updated successfully", ProductId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating product: {Error}", ex.Message);
                return StatusCode(500, new { Message = ex.Message });
            }
        }



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
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetProductsByCategoryId([FromQuery] ProductFilterParams parameters, int categoryId)
        {
            var result = await _productService.GetProductsByCategoryId(parameters, categoryId);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}