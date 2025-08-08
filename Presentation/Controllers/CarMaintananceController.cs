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
    public class CarMaintananceController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarMaintananceController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMaintananceRecord([FromBody] CarMaintananceAllDTO carMaintananceAllDTO)
        {
            if (carMaintananceAllDTO == null)
            {
                return BadRequest("Car maintenance record cannot be null");
            }
            await serviceManager.carMaintananceService.AddMaintananceRecord(carMaintananceAllDTO);
            return Ok(ApiResponse<CarMaintananceAllDTO>.SuccessResponse(null, "Maintanance record Added succefulluy"));
        }




        //// Schedule
        //[HttpPost("Schedule")]

        //public async Task<IActionResult> ScheduleMaintanance(ScheduleDTO maintenanceRequest)
        //{
        //    if (maintenanceRequest == null ||
        //        string.IsNullOrEmpty(maintenanceRequest.ToEmail) ||
        //        string.IsNullOrEmpty(maintenanceRequest.MaintananceType) ||
        //        string.IsNullOrEmpty(maintenanceRequest.Ownername) ||
        //        maintenanceRequest.MaintananceDate == default)
        //    {
        //        return BadRequest("Email, maintenance type, owner name, and maintenance date cannot be null");
        //    }
        //    BackgroundJob.Schedule(()=>
        //            SendEmailAsync(maintenanceRequest),
        //            TimeSpan.FromMinutes(2)); 

        //    return Ok("تم جدولة الصيانة بنجاح");

        //}

        //[HttpGet("send-email")]
        //public async Task SendEmailAsync(ScheduleDTO maintenanceRequest)
        //{
        //    var emailService = serviceManager.emailService;
        //    await emailService.SendEmail(maintenanceRequest.ToEmail , maintenanceRequest.MaintananceType , maintenanceRequest.Ownername , maintenanceRequest.MaintananceDate);
        //}
    }
}
