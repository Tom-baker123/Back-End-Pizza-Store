using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class ToppingService : IToppingService
    {
        private readonly AppDbContext _context;
        public ToppingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseResult>> Create(ToppingCreateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var topping = ToppingMappngs.CreateVModelToEntity(model);
                _context.Toppings.Add(topping);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(model, "Tạo topping thành công");
                return response;
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
                var topping = await _context.Toppings.FindAsync(id);
                if (topping == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                _context.Toppings.Remove(topping);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(topping, "Xóa topping thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<PaginationModel<ToppingGetVModel>>> GetAll()
        {
            var ds = await _context.Toppings.OrderByDescending(c => c.ToppingID)
                .Select(x => ToppingMappngs.EntityToVModel(x)).ToListAsync();
            return new PaginationModel<ToppingGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<ToppingGetVModel>?> GetbyId(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
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
                var size = await _context.Toppings.FindAsync(model.ToppingID);
                if (size == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                size.Name = model.Name;
                size.Price = model.Price;
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(size, "Cập nhật topping thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}
