using Stage1.Model;

namespace Stage1.Publisher
{
    public interface IOrderPublisher
    {
        Task PublishOrderCreatedAsync(Order order);
    }
}
