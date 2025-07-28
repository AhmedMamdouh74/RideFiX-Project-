using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.ReviewsDTOs;
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
    public class ReviewController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ReviewController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDTO review)
        {
            if (review == null)
            {
                return BadRequest("لا يمكن اضافه تقييم فارغ !");
            }
            await serviceManager.reviewService.AddReviewAsync(review);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Review successfully added"));
        }

    }
}
