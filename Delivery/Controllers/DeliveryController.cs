using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Model;
using Delivery.Services;
using System.Xml.Linq;

namespace Delivery.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddDeliveryRequest([FromBody] DeliveryRequest request)
        {
            await _deliveryService.AddDeliveryRequestAsync(request.Id, request.Name, request.Address);
            return Ok("Delivery request added successfully");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDeliveryRequest(Guid id)
        {
            var deliveryRequest = await _deliveryService.GetDeliveryRequestByIdAsync(id);
            if (deliveryRequest == null) return NotFound();
            return Ok(deliveryRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryRequests()
        {
            var deliveryRequests = await _deliveryService.GetAllDeliveryRequestsAsync();
            return Ok(deliveryRequests);
        }
    }
}
