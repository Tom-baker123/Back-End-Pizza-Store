using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<CategoryGetVModel>>> GetAll([FromQuery]CategoryFilterParams parameters)
        {
            var results = await _categoryService.GetAll(parameters);
            return results;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetVModel>> GetById(int id)
        {
            var result = await _categoryService.GetbyId(id);
            if (result == null)
            {
                return NotFound(new ErrorResponseResult("Không tìm thấy danh mục"));
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] CategoryCreateVModel model)
        {
            if (model == null)
            {
                return BadRequest(new ErrorResponseResult("Dữ liệu không hợp lệ"));
            }

            var result = await _categoryService.Create(model);
            if (result is ErrorResponseResult)
            {
                return BadRequest(result);
            }
            return CreatedAtAction(nameof(GetById), new { id = model.CategoryName }, result);
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> Update(int id, [FromBody] CategoryUpdate model)
        {
            return await _categoryService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Remove(int id)
        {
            var result = await _categoryService.Remove(id);
            if (result is ErrorResponseResult)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
