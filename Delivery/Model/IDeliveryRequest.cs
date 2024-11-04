using Delivery.Data;
using Microsoft.EntityFrameworkCore;


namespace Delivery.Model
{
    public class IDeliveryRequest
    {
        public Guid Id { get;}
        public string Name { get; }
        public string Address { get;}
        public string Status { get;}
    }
}