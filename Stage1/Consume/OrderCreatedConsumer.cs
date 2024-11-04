using MassTransit;
using Stage1.Model;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using System.Text.Json;

namespace Stage1.Consume
{
    public class OrderCreatedConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            var jsonMessage = JsonSerializer.Serialize(context.Message);

            Console.WriteLine($"Received message: {jsonMessage}");
            //var message = context.Message;

            //Console.WriteLine($"Получено событие: OrderId = {message.Id}, Name = {message.Name}, Description= {message.Description} ");

            await Task.CompletedTask;
        }
    }
}





