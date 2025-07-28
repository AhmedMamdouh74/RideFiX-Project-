using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
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
    public class RequestController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public RequestController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RealRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request data cannot be null.");
            }
            await serviceManager.requestServices.CreateRealRequest(request);
            return Ok(ApiResponse<string>.SuccessResponse(null, "requests succesfully created"));
        }

        [HttpDelete("CancelAll/{CarOwnerID}")]
        public async Task<IActionResult> CancelAll(int CarOwnerID)
        {
            if (CarOwnerID <= 0)
            {
                return BadRequest("Invalid Car Owner ID.");
            }
            await serviceManager.requestServices.CancelAll(CarOwnerID);
            return Ok(ApiResponse<string>.SuccessResponse(null, "All requests cancelled successfully."));
        }

        [HttpGet("RequestBreifDTOs/{carOwnerID}")]
        public async Task<IActionResult> GetRequestBreifDTOs(int carOwnerID)
        {
            if (carOwnerID <= 0)
            {
                return BadRequest("Invalid Car Owner ID.");
            }
            var requests = await serviceManager.requestServices.RequestBreifDTOs(carOwnerID);
            if (requests == null || !requests.Any())
            {
                return NotFound("No requests found for the specified Car Owner ID.");
            }
            return Ok(ApiResponse<List<RequestBreifDTO>>.SuccessResponse(requests, "Requests retrieved successfully."));
        }

        [HttpGet("RequestDetails/{requestId}")]
        public async Task<IActionResult> GetRequestDetails(int requestId)
        {
            if (requestId <= 0)
            {
                return BadRequest("Invalid Request ID.");
            }
            var requestDetails = await serviceManager.requestServices.RequestDetailsDTOs(requestId);
            if (requestDetails == null)
            {
                return NotFound("Request not found.");
            }
            return Ok(ApiResponse<RequestDetailsDTO>.SuccessResponse(requestDetails, "Request details retrieved successfully."));
        }

        [HttpPost("CompleteRequest/{requestId}")]
        public async Task<IActionResult> CompleteRequest(int requestId)
        {
            if (requestId <= 0)
            {
                return BadRequest("Invalid Request ID.");
            }
            await serviceManager.requestServices.CompleteRequest(requestId);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Request completed successfully."));

        }

        //[HttpGet("request/{id}")]
        //public async Task<IActionResult> IsPresentRequest(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest("Invalid Request ID.");
        //    }
        //    var request = await serviceManager.requestServices.GetRequestById(id);
        //    if (request == null)
        //    {
        //        return NotFound("Request not found.");
        //    }
        //    return Ok(ApiResponse<RealRequestDTO>.SuccessResponse(request, "Request retrieved successfully."));
        //}
    }
}
