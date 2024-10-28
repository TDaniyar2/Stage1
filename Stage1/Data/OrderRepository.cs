using Microsoft.EntityFrameworkCore;
using Stage1.Model;

namespace Stage1.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbTaskContext _dbContext;

        public OrderRepository(DbTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task AddAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
