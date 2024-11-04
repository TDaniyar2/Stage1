namespace Delivery.Model
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}