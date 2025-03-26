using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.ViewModels;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll()
        {
            var products = await _context.Products.OrderByDescending(p => p.Id)
                .Include(p => p.Category)
                .Select(p => new ProductGetVModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryID = p.CategoryID,
                    //Category = new CategoryGetVModel
                    //{
                    //    CategoryId = p.Category.CategoryID,
                    //    CategoryName = p.Category.CategoryName
                    //},
                    ImageURL = p.ImageURL
                }).ToListAsync();

            return new PaginationModel<ProductGetVModel>
            {
                Records = products,
                TotalRecords = products.Count
            };
        }

        public async Task<ActionResult<ProductGetVModel>?> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return new ProductGetVModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryID = product.CategoryID,
                //Category = new CategoryGetVModel
                //{
                //    CategoryId = product.Category.CategoryID,
                //    CategoryName = product.Category.CategoryName
                //},
                ImageURL = product.ImageURL
            };
        }

        public async Task<ResponseResult> Create(ProductCreateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryID = model.CategoryID,
                    ImageURL = model.ImageURL
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(model, "Tạo sản phẩm thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(int id, ProductUpdateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return new ErrorResponseResult("Không tìm thấy sản phẩm");
                }
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryID = model.CategoryID;
                product.ImageURL = model.ImageURL;
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(product, "Cập nhật sản phẩm thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Remove(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ErrorResponseResult("Không tìm thấy sản phẩm");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new SuccessResponseResult("Xóa sản phẩm thành công");
        }
    }
}