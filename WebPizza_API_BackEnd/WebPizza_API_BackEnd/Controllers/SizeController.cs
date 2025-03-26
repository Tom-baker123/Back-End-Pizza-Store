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
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationModel<SizeGetVModel>>> GetAll()
        {
            var results = await _sizeService.GetAll();
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeGetVModel>?> GetbyId(int id)
        {
            var result = await _sizeService.GetbyId(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create(SizeCreateVModel model)
        {
            var result = await _sizeService.Create(model);
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseResult>> Update(int id, SizeUpdateVModel model)
        {
            var result = await _sizeService.Update(model);
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var result = await _sizeService.Delete(id);
            return result;
        }
    }
}
