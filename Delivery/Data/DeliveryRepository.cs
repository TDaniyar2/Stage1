using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delivery.Data;
using Delivery.Model;
using Microsoft.EntityFrameworkCore;
using Delivery.Model;

namespace Delivery.Data
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryContext _context;

        public DeliveryRepository(DeliveryContext context)
        {
            _context = context;
        }

        public async Task<DeliveryRequest> AddDeliveryRequestAsync(DeliveryRequest deliveryRequest)
        {
            _context.DeliveryRequests.Add(deliveryRequest);
            await _context.SaveChangesAsync();
            return deliveryRequest; 
        }

        public async Task<DeliveryRequest> GetDeliveryRequestByIdAsync(Guid id)
        {
            return await _context.DeliveryRequests.FindAsync(id);
        }

        public async Task<IEnumerable<DeliveryRequest>> GetAllDeliveryRequestsAsync()
        {
            return await _context.DeliveryRequests.ToListAsync();
        }
    }
}