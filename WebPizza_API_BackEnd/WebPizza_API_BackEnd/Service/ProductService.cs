using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.ViewModels.Order;

//using WebPizza_API_BackEnd.ViewModels;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }

        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll(ProductFilterParams parameters)

        {
            var products = await _productRepository.GetAllAsync();

            var ds = products.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                 .Take(parameters.PageSize).Select(x => ProductMapper.EntityToVModel(x)).ToList();

            return new PaginationModel<ProductGetVModel>
            {
                 Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<ProductGetVModel>?> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
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
                ImageURL = product.ImageURL
            };
        }

        public async Task<ResponseResult> Create(ProductCreateVModel model, string imageUrl)
        {
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryID = model.CategoryID,
                    ImageURL = imageUrl // Sửa từ model.ImageURL sang imageUrl
                };

                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                return new SuccessResponseResult(product.Id, "Tạo sản phẩm thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(int id, ProductUpdateVModel model)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return new ErrorResponseResult("Không tìm thấy sản phẩm");
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryID = model.CategoryID;
                product.ImageURL = model.ImageURL;

                await _productRepository.UpdateAsync(product);
                return new SuccessResponseResult(product, "Cập nhật sản phẩm thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Remove(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return new ErrorResponseResult("Không tìm thấy sản phẩm");
                }

                await _productRepository.DeleteAsync(product);
                return new SuccessResponseResult("Xóa sản phẩm thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
         public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _productRepository.GetProductByNameAsync(name);
        }
        public async Task<ProductModel> GetProductWithDetailsAsync(int id)
        {
            var product = await _productRepository.GetProductWithDetailsAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return _mapper.Map<ProductModel>(product);
        }
        public async  Task<ActionResult<PaginationModel<ProductGetVModel>>> GetProductsByCategoryId(ProductFilterParams parameters, int categoryId)
        {
            var products = await _productRepository.GetAllAsync();

            var ds = products
                     .Where(x => x.CategoryID == categoryId)   
                     .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                     .Take(parameters.PageSize)
                     .Select(x => ProductMapper.EntityToVModel(x)).ToList();

            return new PaginationModel<ProductGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }
    }
}