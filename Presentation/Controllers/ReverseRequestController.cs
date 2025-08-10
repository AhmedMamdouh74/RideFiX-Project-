using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.Notifications;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class ReverseRequestController: ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ReverseRequestController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetReverseRequest()
        {

            var reverseRequests = await serviceManager.reverserRequestService.GetReverserequest();
            if (reverseRequests == null || !reverseRequests.Any())
            {
                return NotFound("No reverse requests found.");
            }
            return Ok(ApiResponse<List<NotificationDto>>.SuccessResponse(reverseRequests, "reverse requests reviled succesfully"));



        }
    }
}
