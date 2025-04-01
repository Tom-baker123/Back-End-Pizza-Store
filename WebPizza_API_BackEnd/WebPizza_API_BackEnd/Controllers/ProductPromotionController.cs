using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPromotionController : ControllerBase
    {
        private readonly IProductPromotionService _productPromotionService;
        public ProductPromotionController(IProductPromotionService productPromotionService)
        {
            _productPromotionService = productPromotionService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationModel<ProductPromotionGetVModel>>> GetAll([FromQuery] ProductPromotionFilterParams parameters)
        {
            var results = await _productPromotionService.GetAll(parameters);
            return results;
        }
        [HttpGet("{productId}/{promotionId}")]
        public async Task<ActionResult<ProductPromotionGetVModel>> GetById(int productId, int promotionId)
        {
            var result = await _productPromotionService.GetbyId(productId, promotionId);
            if (result == null)
            {
                return NotFound(new ErrorResponseResult("Không tìm thấy promotion"));
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] ProductPromotionCreateVModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productPromotionService.Create(model);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }
            return Ok(result);
        }
        [HttpDelete("{productId}/{promotionId}")]
        public async Task<ActionResult<ResponseResult>> Delete(int productId, int promotionId)
        {
            var result = await _productPromotionService.Remove(productId, promotionId);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }
            return Ok(result);
        }
        //[HttpPut("{productId}/{promotionId}")]
        //public async Task<ActionResult<ResponseResult>> Update(int productId, int promotionId, [FromBody] ProductPromotionUpdateVModel model)
        //{
        //    var result = await _productPromotionService.Update(productId, promotionId, model);
        //    if (result is ErrorResponseResult errorResult)
        //    {
        //        return BadRequest(errorResult);
        //    }
        //    return Ok(result);
        
    }
}
