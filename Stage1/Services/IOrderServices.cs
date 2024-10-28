
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stage1.Model;


namespace Stage1.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<Order> CreateOrderAsync(Order newOrder);
        Task UpdateOrderAsync(Guid id, Order updatedOrder);
        Task DeleteOrderAsync(Guid id);
    }
}
