using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.E_CommerceDTOs;
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
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ProductController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllProducts(
            int? pageNumber, 
            int? itemPerPage,
            decimal? maxPrice = null, 
            int? categoryId = null)
        {
            var products = await serviceManager.productsService
                .FilterProductsAsync(pageNumber, itemPerPage, maxPrice, categoryId);
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(ApiResponse<List<ProductBreifDTO>>.SuccessResponse(products, "Requests successfully created"));
        }


        [HttpGet]
        [Route("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Product ID must be greater than zero.");
            }
            var product = await serviceManager.productsService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound($"Product with ID {productId} not found.");
            }
            return Ok(ApiResponse<CartItemDto>.SuccessResponse(product, "Requests successfully created"));
        }
    }
}


