using MassTransit;
using Stage1.Model;


namespace Stage1.Publisher
{
    public class OrderPublisher : IOrderPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishOrderCreatedAsync(Order order)
        {
            await _publishEndpoint.Publish(order);
        }
    }
}
