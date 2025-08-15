using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.CarMaintananceDTOs;
using SharedData.DTOs.MTypesDtos;
using SharedData.Wrapper;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintainanceTypesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public MaintainanceTypesController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Mtypes = await serviceManager.maintenanceTypesService.GetAll();
            return Ok(ApiResponse<List<MaintenanceTypeDTO>>.SuccessResponse(Mtypes, "Maint"));
        }
    }
}
