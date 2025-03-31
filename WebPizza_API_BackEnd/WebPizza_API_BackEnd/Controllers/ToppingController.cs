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
    public class ToppingController : ControllerBase
    {
        private readonly IToppingService _toppingService;
        public ToppingController(IToppingService toppingService)
        {
            _toppingService = toppingService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationModel<ToppingGetVModel>>> GetAll([FromQuery]ToppingFilterParams parameters)
        {
            var results = await _toppingService.GetAll(parameters);
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingGetVModel>?> GetbyId(int id)
        {
            var result = await _toppingService.GetbyId(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create(ToppingCreateVModel model)
        {
            var result = await _toppingService.Create(model);
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var result = await _toppingService.Delete(id);
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseResult>> Update(int id, ToppingUpdateVModel model)
        {
            var result = await _toppingService.Update(model);
            return result;
        }
    }
}
