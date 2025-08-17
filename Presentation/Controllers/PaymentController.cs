using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.Payment;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public PaymentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("PaymentInt/{Id}")]
        public async Task<IActionResult> ProcessPayment(int Id)
        {
            var result = await _serviceManager.paymentService.CreateOrUpdatePaymentIntentRideCoinsAsync(Id);
            return Ok(result);
        }
    }
}
