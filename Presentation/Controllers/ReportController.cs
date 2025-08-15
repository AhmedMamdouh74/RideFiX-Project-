using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.ReportDtos;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ReportController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }


        [HttpPost]
        public async Task<IActionResult> AddReportAsync(CreateReportDto reportDto)
        {
            if (reportDto == null)
            {
                return BadRequest("Report data cannot be null");
            }
            try
            {
                await serviceManager.reportsServices.AddReportAsync(reportDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
