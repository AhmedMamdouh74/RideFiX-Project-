using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.E_CommerceDTOs;
using SharedData.Wrapper;


namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
  
    public class ProductCategoryController:  ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ProductCategoryController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductCategories()
        {
            var productCategories = await serviceManager.productCategoryService.GetAllProductCategoriesAsync();
            if (productCategories == null || !productCategories.Any())
            {
                return NotFound("No product categories found.");
            }
            return Ok(ApiResponse<List<ProductCategoryDto>>.SuccessResponse(productCategories, "requests succesfully created"));
        }

    }
}
