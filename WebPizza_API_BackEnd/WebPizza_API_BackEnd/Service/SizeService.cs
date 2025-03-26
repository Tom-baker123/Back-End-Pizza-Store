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
    public class SizeService : ISizeService
    {
        private readonly AppDbContext _context;
        public SizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseResult>> Create(SizeCreateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var size = SizeMappings.CreateVModelToEntity(model);
                _context.Sizes.Add(size);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(model, "Tạo size thành công");
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
                var size = await _context.Sizes.FindAsync(id);
                if(size == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                _context.Sizes.Remove(size);
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(size, "Xóa size thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<PaginationModel<SizeGetVModel>>> GetAll()
        {
                var ds = await _context.Sizes.OrderByDescending(c => c.SizeID)
                    .Select(x => SizeMappings.EntityToVModel(x)).ToListAsync();
                return new PaginationModel<SizeGetVModel>
                {
                    Records = ds,
                    TotalRecords = ds.Count
                };
        }

        public async Task<ActionResult<SizeGetVModel>?> GetbyId(int id)
        {
            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return null;
            }
            return SizeMappings.EntityToVModel(size);
        }

        public async Task<ActionResult<ResponseResult>> Update(SizeUpdateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var size = await _context.Sizes.FindAsync(model.SizeID);
                if (size == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }
                size.Name = model.Name;
                size.AdditionalPrice = model.AdditionalPrice;
                await _context.SaveChangesAsync();
                response = new SuccessResponseResult(size, "Cập nhật size thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}
