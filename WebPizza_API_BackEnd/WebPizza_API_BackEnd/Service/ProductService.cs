﻿using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.ViewModels;

namespace WebPizza_API_BackEnd.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepository;

        public ProductService(IProductRepo productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();

            var productViewModels = products.Select(p => new ProductGetVModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryID = p.CategoryID,
                ImageURL = p.ImageURL
            }).ToList();

            return new PaginationModel<ProductGetVModel>
            {
                Records = productViewModels,
                TotalRecords = productViewModels.Count
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

        public async Task<ResponseResult> Create(ProductCreateVModel model)
        {
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

                await _productRepository.AddAsync(product);
                return new SuccessResponseResult(model, "Tạo sản phẩm thành công");
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
    }
}