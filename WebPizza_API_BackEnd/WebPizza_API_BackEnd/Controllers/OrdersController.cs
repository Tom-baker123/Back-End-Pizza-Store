using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.ViewModels.Order;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderVModel>>> GetOrders()
        {
            return Ok(await _orderService.GetOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderVModel>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderVModel>> PostOrder(OrderVModel orderVm)
        {
            var createdOrder = await _orderService.AddOrderAsync(orderVm);
            return CreatedAtAction("GetOrder", new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderVModel orderVm)
        {
            if (id != orderVm.OrderID) return BadRequest();
            var result = await _orderService.UpdateOrderAsync(orderVm);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
