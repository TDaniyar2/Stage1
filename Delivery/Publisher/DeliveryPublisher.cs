using MassTransit;
using Delivery.Model;
using Delivery.Data;

public class DeliveryPublisher : IDeliveryPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DeliveryPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishDeliveryRequestCreated(OrderCreated ordercreated)
    {
        await _publishEndpoint.Publish(ordercreated);
    }
}