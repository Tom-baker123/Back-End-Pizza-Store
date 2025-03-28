using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepo _sizeRepository;

        public SizeService(ISizeRepo sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<ActionResult<PaginationModel<SizeGetVModel>>> GetAll()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            var sizeViewModels = sizes.Select(x => SizeMappings.EntityToVModel(x)).ToList();

            return new PaginationModel<SizeGetVModel>
            {
                Records = sizeViewModels,
                TotalRecords = sizeViewModels.Count
            };
        }

        public async Task<ActionResult<SizeGetVModel>?> GetbyId(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null)
            {
                return null;
            }
            return SizeMappings.EntityToVModel(size);
        }

        public async Task<ActionResult<ResponseResult>> Create(SizeCreateVModel model)
        {
            try
            {
                var size = SizeMappings.CreateVModelToEntity(model);
                await _sizeRepository.AddAsync(size);
                return new SuccessResponseResult(model, "Tạo size thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<ResponseResult>> Update(SizeUpdateVModel model)
        {
            try
            {
                var size = await _sizeRepository.GetByIdAsync(model.SizeID);
                if (size == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }

                size.Name = model.Name;
                size.AdditionalPrice = model.AdditionalPrice;
                await _sizeRepository.UpdateAsync(size);
                return new SuccessResponseResult(size, "Cập nhật size thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            try
            {
                var size = await _sizeRepository.GetByIdAsync(id);
                if (size == null)
                {
                    return new ErrorResponseResult("Không tìm thấy size");
                }

                await _sizeRepository.DeleteAsync(size);
                return new SuccessResponseResult(size, "Xóa size thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}