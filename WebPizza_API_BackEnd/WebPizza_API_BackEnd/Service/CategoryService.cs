using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<PaginationModel<CategoryGetVModel>>> GetAll()
        {
            var ds = await _context.Categories.OrderByDescending(c => c.CategoryID)
                .Select(x=>CategoryMappings.EntityToVModel(x)).ToListAsync();
            return new PaginationModel<CategoryGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<CategoryGetVModel>?> GetbyId(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return null;
            }
            return CategoryMappings.EntityToVModel(category);
        }

        public async Task<ResponseResult> Create(CategoryCreateVModel model)
        {
            var response= new ResponseResult();
            try
            {
                var category = CategoryMappings.CreateVModelToEntity(model);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(model, "Tạo danh mục thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(CategoryUpdateVModel model)
        {
            var response = new ResponseResult();
            try 
            { 
                var category = await _context.Categories.FindAsync(model.CategoryId);
                if(category == null)
                {
                    return new ErrorResponseResult("Không tìm thấy danh mục");
                }
                category.CategoryName = model.CategoryName;
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(category, "Cập nhật danh mục thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Remove(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return new ErrorResponseResult("Không tìm thấy danh mục");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new SuccessResponseResult("Xóa danh mục thành công");
        }
    }
}
