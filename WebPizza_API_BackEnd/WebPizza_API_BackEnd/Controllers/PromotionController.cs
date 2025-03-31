using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Service;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationModel<PromotionGetVModel>>> GetAll([FromQuery] PromotionFilterParams parameters)
        {
            var results = await _promotionService.GetAll(parameters);
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionGetVModel>> GetById(int id)
        {
            var result = await _promotionService.GetbyId(id);
            if (result == null)
            {
                return NotFound(new ErrorResponseResult("Không tìm thấy promotion"));
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] PromotionCreateVModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _promotionService.Create(model);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }

            return Ok(result);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var result = await _promotionService.Remove(id);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseResult>> Update(int id, [FromBody] PromotionUpdateVModel model)
        {
            var result = await _promotionService.Update(id,model);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }
            return Ok(result);
        }
    }
}
