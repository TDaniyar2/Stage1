using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Stage1;
using Stage1.Data;
using Stage1.Model;


namespace Stage1.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            return order;
        }

        public async Task UpdateOrderAsync(Guid id, Order order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null) throw new KeyNotFoundException("Order not found");

            existingOrder.Name = order.Name;
            existingOrder.Description = order.Description;
            await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}

