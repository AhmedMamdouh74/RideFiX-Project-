using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using SharedData.Wrapper;

namespace RideFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TRequestEmergencyController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public TRequestEmergencyController(IServiceManager _iServiceManager)
        {
            serviceManager = _iServiceManager;
        }

        [HttpGet("details/{requestId}/{technicianId}")]
        [EndpointSummary("Get emegencyRequestdetails by id")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<EmergencyRequestDetailsDTO>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetRequestDetailsAsync(int requestId, int technicianId)
        {
            try
            {
                var request = await serviceManager
                    .technicianRequestEmergency
                    .GetRequestDetailsByIdAsync(requestId, technicianId);

                if (request == null)
                    return NotFound(ApiResponse<string>.FailResponse("Request details not found for this technician and request"));

                return Ok(ApiResponse<EmergencyRequestDetailsDTO>.SuccessResponse(request, "Request details found"));
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}");
                return BadRequest(ApiResponse<string>.FailResponse($"An error occurred: {ex.Message}"));
            }
        }

        [HttpGet("accepted/{technicalId}")]
        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllAcceptedRequests(int technicalId)
        {
            var request = await serviceManager.technicianRequestEmergency.GetAllAcceptedRequestsAsync(technicalId);
            if (request == null)
                return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
        }
        [HttpGet("active/{technicianId}")]

        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllActiveedRequests(int technicianId)
        {
            try
            {
                var request = await serviceManager.technicianRequestEmergency.GetAllActiveRequestsAsync(technicianId);
                if (request == null)
                    return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
                return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.FailResponse($"An error occurred: {ex.Message}"));
            }
        }

        [EndpointSummary("Get RequestsAssignedToTechnician by technicianId if it's waiting state ")]
        [HttpGet("assigned/{technicianId}")]
        public async Task<IActionResult> GetAllRequestsAssignedToTechnician(int technicianId)
        {
            var request = await serviceManager.technicianRequestEmergency.GetAllRequestsAssignedToTechnicianAsync(technicianId);
            if (request == null || !request.Any())
                return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
        }


        [HttpPut]
        [EndpointSummary("Accept or Reject Emergency Request By Technician")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateRequestFromCarOwnerAsync([FromBody] TechnicianUpdateEmergencyRequestDTO dto)
        {
            var result = await serviceManager.technicianRequestEmergency.UpdateRequestFromCarOwnerAsync(dto);


            return Ok(ApiResponse<bool>.SuccessResponse(true, "Updated successfully"));
        }

        [HttpPost]
        [EndpointSummary("Apply request from technical home page")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ApplyRequestFromHomePage([FromBody] TechnicianApplyEmergencyRequestDTO dto)
        {
            var result = await serviceManager.technicianRequestEmergency.ApplyRequestFromHomePage(dto);
            if (!result)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid technician, PIN, or request"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Added successfully"));
        }
        [HttpGet("completed/{technicianId}")]
        [EndpointSummary("get completed requests for history")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<EmergencyRequestDetailsDTO>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]

        public async Task<IActionResult> GetAllCompletedRequests(int technicianId)
        {
            var result = await serviceManager.technicianRequestEmergency.GetAllCompletedRequestsAsync(technicianId);
            if (result == null || result.Count() == 0)
                return NotFound(ApiResponse<string>.FailResponse("you don't have completed requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(result, "technician have Completed requests"));
        }

        [HttpGet("applied/{techId}")]
        [EndpointSummary("Get all applied requests by technician")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<TechReverseRequestDTO>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetTechAllAppliedRequests(int techId)
        {
            try
            {

                var requests = await serviceManager.technicianRequestEmergency.GetTechAllAppliedRequestsAsync(techId);
                if (requests == null || !requests.Any())
                    return BadRequest(ApiResponse<string>.FailResponse("No applied requests found for this technician"));

                return Ok(ApiResponse<List<TechReverseRequestDTO>>.SuccessResponse(requests, "Applied requests found"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest(ApiResponse<string>.FailResponse($"An error occurred: {ex.Message}"));
            }
        }


    }

}
