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

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
