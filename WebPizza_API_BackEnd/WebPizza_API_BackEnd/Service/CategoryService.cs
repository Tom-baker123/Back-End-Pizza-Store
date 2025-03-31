using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebPizza_API_BackEnd.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ActionResult<PaginationModel<CategoryGetVModel>>> GetAll(CategoryFilterParams parameters)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var ds = categories.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).Select(x => CategoryMappings.EntityToVModel(x)).ToList();

            return new PaginationModel<CategoryGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<CategoryGetVModel>?> GetbyId(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : CategoryMappings.EntityToVModel(category);
        }

        public async Task<ResponseResult> Create(CategoryCreateVModel model)
        {
            try
            {
                var category = CategoryMappings.CreateVModelToEntity(model);
                await _categoryRepository.AddAsync(category);
                return new SuccessResponseResult(model, "Tạo danh mục thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(int id, CategoryUpdate model)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id); // Dùng id từ tham số
                if (category == null)
                {
                    return new ErrorResponseResult("Không tìm thấy danh mục");
                }

                category.CategoryName = model.CategoryName; // Chỉ cập nhật tên
                await _categoryRepository.UpdateAsync(category);

                return new SuccessResponseResult(category, "Cập nhật danh mục thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Remove(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ErrorResponseResult("Không tìm thấy danh mục");
            }

            await _categoryRepository.DeleteAsync(category);
            return new SuccessResponseResult("Xóa danh mục thành công");
        }
    }
}
