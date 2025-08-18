using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.credit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.Payment;
using SharedData.Wrapper;

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

        [HttpPost("InitializePayment")]
        public async Task<IActionResult> InitializePayment(int coins)
        {
            var Id = await _serviceManager.paymentService.CreateChargeEntityAsync(coins);
            return Ok(Id);
        }

        [HttpPost("createTransaction/{Id}")]
        public async Task<IActionResult> GetPaymentIntent(int Id)
        {
            await _serviceManager.paymentService.CreateTransaction(Id);
            return Ok(ApiResponse<CoinTopUp>.SuccessResponse(null, "Transaction successfully created"));
        }
    }
}
