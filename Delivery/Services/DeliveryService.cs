using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Data;
using Delivery.Model;
using Delivery.Services;

namespace Delivery.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task AddDeliveryRequestAsync(Guid Id, string Name, string Address)
        {
            var deliveryRequest = new DeliveryRequest
            {
                Id = Id,
                Name = Name,
                Address = Address,
                Status = "Pending"
            };
            await _deliveryRepository.AddDeliveryRequestAsync(deliveryRequest);
        }

        public async Task<DeliveryRequest> GetDeliveryRequestByIdAsync(Guid id)
        {
            return await _deliveryRepository.GetDeliveryRequestByIdAsync(id);
        }

        public async Task<IEnumerable<DeliveryRequest>> GetAllDeliveryRequestsAsync()
        {
            return await _deliveryRepository.GetAllDeliveryRequestsAsync();
        }
    }
}

