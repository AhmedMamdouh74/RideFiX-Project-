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

        [HttpGet("users")]
        [EndpointSummary("Get all users (both technicians and car owners)")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReadUsersDTO>>))]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await serviceManager.adminService.GetAllUsersAsync();
            return Ok(ApiResponse<List<ReadUsersDTO>>.SuccessResponse(users, "Successful request"));
        }

        [HttpDelete("users/{id}")]
        [EndpointSummary("Soft delete a user by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> SoftDeleteUser(string id)
        {
            var result = await serviceManager.adminService.SoftDeleteUserAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("User not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "User soft deleted successfully"));
        }

        [HttpPost("users/{id}/restore")]
        [EndpointSummary("Restore a previously soft deleted user by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> RestoreUser(string id)
        {
            var result = await serviceManager.adminService.RestoreUserAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("User not found or not deleted"));

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
            var category = await serviceManager.adminService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetAllCategories), ApiResponse<ReadTCategoryDTO>.SuccessResponse(category, "Category created successfully"));
        }

        [HttpPut("categories/{id}")]
        [EndpointSummary("Update an existing technician category")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> UpdateCategory(int id, UpdateTCategoryDTO dto)
        {
            var updated = await serviceManager.adminService.UpdateCategoryAsync(id, dto);
           

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Category updated successfully"));
        }

        [HttpDelete("categories/{id}")]
        [EndpointSummary("Soft delete a technician category by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> SoftDeleteCategory(int id)
        {
            var result = await serviceManager.adminService.SoftDeleteCategoryAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Category not found"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Category soft deleted successfully"));
        }

        [HttpPost("categories/{id}/restore")]
        [EndpointSummary("Restore a previously soft deleted category by ID")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> RestoreCategory(int id)
        {
            var result = await serviceManager.adminService.RestoreCategoryAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Category not found or not deleted"));

            return Ok(ApiResponse<string>.SuccessResponse(null, "Category restored successfully"));
        }
    }
}
