using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Model;

namespace Delivery.Services
{
    public interface IDeliveryService
    {
        Task AddDeliveryRequestAsync(Guid Id, string Name, string Address);
        Task<DeliveryRequest> GetDeliveryRequestByIdAsync(Guid id);
        Task<IEnumerable<DeliveryRequest>> GetAllDeliveryRequestsAsync();
    }
}
