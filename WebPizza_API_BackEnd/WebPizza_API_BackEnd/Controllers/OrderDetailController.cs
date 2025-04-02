using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Service;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<PaginationModel<OrderDetailGetVModel>>> GetAll([FromQuery] OrderDetailFilterParams parameters)
        {
            {
                var result = await _orderDetailService.GetAll(parameters);
                return result;
            }
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailGetVModel>> GetById(int id)
        {
            var result = await _orderDetailService.GetById(id);

            if (result.Value == null)
            {
                return NotFound();
            }

            return result.Value;
        }

        // POST: api/OrderDetails
        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create([FromBody] OrderDetailCreateVModel model)
        {
            var result = await _orderDetailService.Create(model);

            if (result.Value is ErrorResponseResult)
            {
                return BadRequest(result.Value);
            }

            return CreatedAtAction(nameof(GetById), new { id = ((OrderDetailGetVModel)result.Value.Data).OrderDetailID }, result.Value);
        }

        // PUT: api/OrderDetails/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseResult>> Update(int id, [FromBody] OrderDetailUpdateVModel model)
        {
            if (id != model.OrderDetailID)
            {
                return BadRequest("ID không khớp");
            }

            var result = await _orderDetailService.Update(model);

            if (result.Value is ErrorResponseResult)
            {
                return BadRequest(result.Value);
            }

            return Ok(result.Value);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseResult>> Delete(int id)
        {
            var result = await _orderDetailService.Delete(id);

            if (result.Value is ErrorResponseResult)
            {
                return BadRequest(result.Value);
            }

            return Ok(result.Value);
        }
    }
}