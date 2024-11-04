using MassTransit;
using Delivery.Model;
using Delivery.Data;

namespace Delivery.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly DeliveryContext _context;

        public OrderCreatedConsumer(DeliveryContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var message = context.Message;

            var deliveryRequest = new DeliveryRequest
            {
                Id = Guid.NewGuid(),  
                Address = message.Address, 
                Status = "Pending" 
            };

            _context.DeliveryRequests.Add(deliveryRequest);
            await _context.SaveChangesAsync();
        }
    }
}
