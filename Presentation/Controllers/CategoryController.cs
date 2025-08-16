using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs;
using SharedData.DTOs.TechnicianDTOs;
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
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CategoryController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            var categories = serviceManager.categoryService.GetAllCategoriesAsync().Result;
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            
            return Ok(ApiResponse<List<TCategoryDTO>>.SuccessResponse(categories.ToList() , "available categories"));
        }

    }
}
