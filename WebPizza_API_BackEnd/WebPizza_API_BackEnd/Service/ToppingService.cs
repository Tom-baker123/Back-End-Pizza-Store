using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class ToppingService : IToppingService
    {
        private readonly ITopingRepo _topingRepo;
        public ToppingService(ITopingRepo topingRepo)
        {
            _topingRepo = topingRepo;
        }

        public async Task<ActionResult<ResponseResult>> Create(ToppingCreateVModel model)
        {
            try
            {
                var topping = ToppingMappngs.CreateVModelToEntity(model);
                await _topingRepo.AddAsync(topping);
                return new SuccessResponseResult(model, "Tạo topping thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var response = new ResponseResult();
            try
            {
                var topping = await _topingRepo.GetByIdAsync(id);
                if (topping == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                await _topingRepo.DeleteAsync(topping);
                response = new SuccessResponseResult(topping, "Xóa topping thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<PaginationModel<ToppingGetVModel>>>  GetAll(ToppingFilterParams parameters)
        {
            var toppings = await _topingRepo.GetAllAsync();
            var ds = toppings.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                 .Take(parameters.PageSize).Select(x => ToppingMappngs.EntityToVModel(x)).ToList();

            return new PaginationModel<ToppingGetVModel>
            {
                 Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<ToppingGetVModel>?> GetbyId(int id)
        {
            var topping = await _topingRepo.GetByIdAsync(id);
            if (topping == null)
            {
                return null;
            }
            return ToppingMappngs.EntityToVModel(topping);
        }

        public async Task<ActionResult<ResponseResult>> Update(ToppingUpdateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var topping = await _topingRepo.GetByIdAsync(model.ToppingID);
                if (topping == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                topping.Name = model.Name;
                topping.Price = model.Price;
                await _topingRepo.UpdateAsync(topping);
                response = new SuccessResponseResult(topping, "Cập nhật topping thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}
