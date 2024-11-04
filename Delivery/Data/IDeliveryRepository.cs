using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Model;


namespace Delivery.Data
{
    public interface IDeliveryRepository
    {
        Task<DeliveryRequest> AddDeliveryRequestAsync(DeliveryRequest deliveryRequest);
        Task<DeliveryRequest> GetDeliveryRequestByIdAsync(Guid id);
        Task<IEnumerable<DeliveryRequest>> GetAllDeliveryRequestsAsync();
    }
}
