using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Stage1.Data;
using Stage1.Model;
using Microsoft.EntityFrameworkCore;
using Stage1.Services;
using MassTransit;
using MassTransit.Transports;
using Stage1.Consume;
using Stage1.Publisher;

namespace Stage1.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IOrderService orderService, IPublishEndpoint publishEndpoint)
        {
            _orderService = orderService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order newOrder)
        {

            var savedOrder = await _orderService.CreateOrderAsync(newOrder);

            
            var orderCreatedEvent = new Order
            {
                Id = savedOrder.Id,
                Name = savedOrder.Name,
                Description = savedOrder.Description
            };

            
            await _publishEndpoint.Publish(orderCreatedEvent);

            return CreatedAtAction(nameof(GetOrder), new { id = savedOrder.Id }, savedOrder);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order updatedOrder)
        {
            await _orderService.UpdateOrderAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}