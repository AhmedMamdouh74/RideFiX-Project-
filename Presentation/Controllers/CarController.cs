using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarDetails()
        {
            return null;
            //return Ok(ApiResponse<RequestBreifDTO>.SuccessResponse(Request, "Has a Request"));
        }
    }
}
