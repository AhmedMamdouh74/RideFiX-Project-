using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.CarMaintananceDTOs;
using SharedData.DTOs.ChatSessionDTOs;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using Microsoft.AspNetCore.Authorization;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarMaintananceController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarMaintananceController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpPost]
      
        public async Task<IActionResult> AddMaintananceRecord([FromBody] CarMaintananceAllDTO carMaintananceAllDTO)
        {
            if (carMaintananceAllDTO == null)
            {
                return BadRequest("Car maintenance record cannot be null");
            }
            await serviceManager.carMaintananceService.AddMaintananceRecord(carMaintananceAllDTO);
            return Ok(ApiResponse<CarMaintananceAllDTO>.SuccessResponse(null, "Maintanance record Added succefulluy"));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMaintenanceSummary()
        {
            var list = await serviceManager.carMaintananceService.GetMaintenanceSummary();
            return Ok(ApiResponse<List<MaintenanceSummaryDTO>>.SuccessResponse(list, "Maintanance Summary"));
        }

        [HttpGet("history/{maintananceId:int}")]
        public async Task<IActionResult> GetAllMaintananceHistoryByID(int maintananceId)
        {
            if (maintananceId <= 0)
            {
                return BadRequest("Invalid maintenance ID");
            }
            var maintananceHistory = await serviceManager.carMaintananceService.GetAllMaintananceHistoryByID(maintananceId);
            if (maintananceHistory == null || !maintananceHistory.Any())
            {
                return NotFound("No maintenance history found for the given ID");
            }
            return Ok(ApiResponse<List<MaintananceHistory>>.SuccessResponse(maintananceHistory, "Maintenance history retrieved successfully"));
        }
    }
}
