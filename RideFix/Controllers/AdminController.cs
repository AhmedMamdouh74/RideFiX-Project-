using Microsoft.AspNetCore.Mvc;
using Service.CoreServices.Admin;
using ServiceAbstraction;
using SharedData.DTOs.Admin.Users;
using SharedData.Wrapper;

namespace RideFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public AdminController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users=await serviceManager.adminService.GetAllUsersAsync();
            return Ok(ApiResponse<List<ReadUsersDTO>>.SuccessResponse(users,"successfull Request"));
        }
     
    }
}
