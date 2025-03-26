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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<CategoryGetVModel>>> GetAll()
        {
            var results = await _categoryService.GetAll();
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetVModel>?> GetbyId(int id)
        {
            var result = await _categoryService.GetbyId(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public async Task<ResponseResult> Create(CategoryCreateVModel model)
        {
            var result = await _categoryService.Create(model);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> Update(CategoryUpdateVModel model)
        {
            var result = await _categoryService.Update(model);
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ResponseResult> Remove(int id)
        {
            var result = await _categoryService.Remove(id);
            return result;
        }
    }
}
