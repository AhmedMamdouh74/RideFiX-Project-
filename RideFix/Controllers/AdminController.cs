using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.Admin.TechnicianCategory;
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

        //  Users 

        [HttpGet("carOwners")]
        [EndpointSummary("Get all car owner users")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReadUsersDTO>>))]
        public async Task<IActionResult> GetAllCarOwnerUsers()
        {
            var users = await serviceManager.adminService.GetAllCarOwnersAsync();
            return Ok(ApiResponse<List<ReadUsersDTO>>.SuccessResponse(users, "Successful request"));
        }

        [HttpGet("technicians")]
        [EndpointSummary("Get all technician users")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReadUsersDTO>>))]
        public async Task<IActionResult> GetAllTechnicianUsers()
        {
            var users = await serviceManager.adminService.GetAllTechniciansAsync();
            return Ok(ApiResponse<List<ReadUsersDTO>>.SuccessResponse(users, "Successful request"));
        }

        [HttpPost("carOwner/{id}")]
        [EndpointSummary("ban car onwer by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> BanCarOwnerUser(int id)
        {
            await serviceManager.adminService.BanCarOwnerAsync(id);


            return Ok(ApiResponse<string>.SuccessResponse(null, "User soft deleted successfully"));
        }

        [HttpPost("tecnician/{id}")]
        [EndpointSummary("ban car onwer by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> BanTechnicianUser(int id)
        {
            await serviceManager.adminService.BanTechnianAsync(id);


            return Ok(ApiResponse<string>.SuccessResponse(null, "User soft deleted successfully"));
        }

        [HttpPost("carOwner/{id}/active")]
        [EndpointSummary("active a previously baned user by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ActiveCarOwnerUser(int id)
        {
            await serviceManager.adminService.ActivateCarOwonerAsync(id);


            return Ok(ApiResponse<string>.SuccessResponse(null, "User restored successfully"));
        }



        [HttpPost("technician/{id}/active")]
        [EndpointSummary("active a previously baned user by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ActiveTechnicianUser(int id)
        {
            await serviceManager.adminService.ActivateTechnianAsync(id);


            return Ok(ApiResponse<string>.SuccessResponse(null, "User restored successfully"));
        }

        // Categories 

        [HttpGet("categories")]
        [EndpointSummary("Get all technician categories")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReadTCategoryDTO>>))]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await serviceManager.adminService.GetAllCategoriesAsync();
            return Ok(ApiResponse<List<ReadTCategoryDTO>>.SuccessResponse(categories, "Successful request"));
        }

        [HttpPost("categories")]
        [EndpointSummary("Create a new technician category")]
        [ProducesResponseType(201, Type = typeof(ApiResponse<ReadTCategoryDTO>))]
        public async Task<IActionResult> CreateCategory(CreateTCategoryDTO dto)
        {
            await serviceManager.adminService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetAllCategories), ApiResponse<string>.SuccessResponse("", "Category created successfully"));
        }

        [HttpPut("categories/{id}")]
        [EndpointSummary("Update an existing technician category")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> UpdateCategory(int id, UpdateTCategoryDTO dto)
        {
            await serviceManager.adminService.UpdateCategoryAsync(id, dto);


            return Ok(ApiResponse<bool>.SuccessResponse(true, "Category updated successfully"));
        }

        [HttpDelete("categories/{id}")]
        [EndpointSummary("Soft delete a technician category by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> SoftDeleteCategory(int id)
        {
            await serviceManager.adminService.DeleteCategoryAsync(id);


            return Ok(ApiResponse<string>.SuccessResponse(null, "Category soft deleted successfully"));
        }

        [HttpGet("users-count")]
        public async Task<IActionResult> GetUsersCount()
        {
            var counts = await serviceManager.adminService.GetUsersCountAsync();
            return Ok(counts);
        }
        [HttpGet("requests-count")]
        public async Task<IActionResult> GetRequestsCount()
        {
            var counts = await serviceManager.adminService.GetrequestsCountAsync();
            return Ok(counts);
        }

    }
}
