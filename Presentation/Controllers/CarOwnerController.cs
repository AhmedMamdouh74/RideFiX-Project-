using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;
namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CarOwnerController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarOwnerController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> CheckRequest(int Id)
        {
            var Request = await serviceManager.carOwnerService.IsRequested(Id);
            return Ok(ApiResponse<RequestBreifDTO>.SuccessResponse(Request, "Has a Request"));
        }

    }
}