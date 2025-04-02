using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ActionResult<PaginationModel<OrderDetailGetVModel>>> GetAll(OrderDetailFilterParams parameters)
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();

            var ds = orderDetails.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                 .Take(parameters.PageSize).Select(x => OrderDetailMapping.EntityToVModel(x)).ToList();

            return new PaginationModel<OrderDetailGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<OrderDetailGetVModel>?> GetById(int id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return null;
            }
            return OrderDetailMapping.EntityToVModel(orderDetail);
        }

        public async Task<ActionResult<ResponseResult>> Create(OrderDetailCreateVModel model)
        {
            try
            {
                var orderDetail = OrderDetailMapping.CreateVModelToEntity(model);
                await _orderDetailRepository.AddAsync(orderDetail);
                return new SuccessResponseResult(model, "Tạo OrderDetail thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<ResponseResult>> Update(OrderDetailUpdateVModel model)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetByIdAsync(model.OrderDetailID);
                if (orderDetail == null)
                {
                    return new ErrorResponseResult("Không tìm thấy OrderDetail");
                }

                orderDetail.OrderID = model.OrderID;
                orderDetail.ProductID = model.ProductID;
                orderDetail.Quantity = model.Quantity;
                orderDetail.BasePrice = model.BasePrice;
                orderDetail.Subtotal = model.Subtotal;
                await _orderDetailRepository.UpdateAsync(orderDetail);
                return new SuccessResponseResult(orderDetail, "Cập nhật OrderDetail thành công");
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
                var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
                if (orderDetail == null)
                {
                    return new ErrorResponseResult("Không tìm thấy OrderDetail");
                }

                await _orderDetailRepository.DeleteAsync(orderDetail);
                return new SuccessResponseResult(orderDetail, "Xóa OrderDetail thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}