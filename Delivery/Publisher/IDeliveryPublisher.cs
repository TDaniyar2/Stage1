using MassTransit;
using Delivery.Model;
using Delivery.Data;

public interface IDeliveryPublisher
{
    Task PublishDeliveryRequestCreated(OrderCreated ordercreated);
}
