using Microsoft.AspNetCore.Mvc;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        public async Task<IActionResult> Create([FromBody] ProductCreateVModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.Create(model);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }

            return Ok(result);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateVModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.Update(id, model);
            if (result is ErrorResponseResult errorResult)
            {
                return BadRequest(errorResult);
            }

            return Ok(result);
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
    }
}