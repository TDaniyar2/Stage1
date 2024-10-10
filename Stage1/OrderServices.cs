using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Stage1;

namespace Stage1
{
    public static class OrderService
    {
        public async static Task CreateOrder(HttpResponse response, HttpRequest request)
        {
            try
            {
                var order = await request.ReadFromJsonAsync<Order>();
                if (order != null)
                {
                    using (var db = new DbTaskContext())
                    {
                        db.Orders.Add(new Order { Name = order.Name, Description = order.Description });
                        await db.SaveChangesAsync();
                        await response.WriteAsJsonAsync(order);
                        await db.Database.CurrentTransaction.CommitAsync();
                    }
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
            }
        }

        public async static Task DeleteOrder(Guid id, HttpResponse response)
        {
            using (var db = new DbTaskContext())
            {
                var order = await db.Orders.FindAsync(id);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    await db.SaveChangesAsync();
                    await response.WriteAsJsonAsync(order);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Заказ не найден" });
                }
            }
        }

        public async static Task GetAllOrders(HttpResponse response)
        {
            using (var db = new DbTaskContext())
            {
                var orders = db.Orders.ToList();

                await response.WriteAsJsonAsync(orders);
            }
        }

        public async static Task GetOrder(Guid id, HttpResponse response)
        {
            using (var db = new DbTaskContext())
            {
                var order = await db.Orders.FindAsync(id); 
                if (order != null)
                {
                    await response.WriteAsJsonAsync(order);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Заказ не найден" });
                }
            }
        }

        public async static Task UpdateOrder(HttpResponse response, HttpRequest request)
        {
            try
            {

                Order? orderRequest = await request.ReadFromJsonAsync<Order>();

                if (orderRequest != null)
                {
                    using (var db = new DbTaskContext())
                    {

                        var orders = await db.Orders.ToListAsync();


                        var order = orders.FirstOrDefault(u => u.Id == orderRequest.Id);

                        if (order != null)
                        {

                            order.Name = orderRequest.Name; 
                            order.Description = orderRequest.Description; 


                            await db.SaveChangesAsync();


                            await response.WriteAsJsonAsync(order);
                        }
                        else
                        {
                            response.StatusCode = 404;
                            await response.WriteAsJsonAsync(new { message = "Заказ не найден" });
                        }
                    }
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
            }
        }
    }
}

