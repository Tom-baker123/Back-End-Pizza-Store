using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] CartCreateVModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new ErrorResponseResult("Dữ liệu không hợp lệ"));
                }
                if(model.Quantity <= 0)
                {
                    return BadRequest(new ErrorResponseResult("Số lượng phải lớn hơn 0"));
                }
                var result = await _cartService.Create(model);
                return result;

            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<PaginationModel<CartGetVModel>>> GetAll()
        {
            var results = await _cartService.GetAll();
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CartGetVModel>?> GetbyId(int id)
        {
            var result = await _cartService.GetbyId(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Remove(int id)
        {
            var result = await _cartService.Remove(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseResult>> Update(int id, CartUpdateVModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new ErrorResponseResult("Dữ liệu không hợp lệ"));
                }
                if (model.Quantity <= 0)
                {
                    return BadRequest(new ErrorResponseResult("Số lượng phải lớn hơn 0"));
                }
                var result = await _cartService.Update(id, model);
                return result;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}
