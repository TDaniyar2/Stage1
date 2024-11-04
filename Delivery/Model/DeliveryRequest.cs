using Delivery.Data;
using Microsoft.EntityFrameworkCore;


namespace Delivery.Model
{
    public class DeliveryRequest : IDeliveryRequest
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
