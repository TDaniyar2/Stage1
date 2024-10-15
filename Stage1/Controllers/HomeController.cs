using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Stage1.Data;
using Stage1.Model;
using Microsoft.EntityFrameworkCore;

namespace Stage1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbTaskContext _dbContext;

        public HomeController(DbTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/api/orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return Json(orders);  
        }

        [HttpGet("/api/orders/{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();  
            }
            return Json(order);  
        }

        [HttpPost("/api/orders")]
        public async Task<IActionResult> CreateOrder([FromBody] Order newOrder)
        {
            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder); 
        }

        [HttpPut("/api/orders/{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order updatedOrder)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();  
            }

            
            order.Name = updatedOrder.Name;
            order.Description = updatedOrder.Description;
            

            await _dbContext.SaveChangesAsync();
            return NoContent();  
        }

        [HttpDelete("/api/orders/{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound(); 
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return NoContent();  
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
