using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.Car;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetCarDetails()
        {
            var Car = await serviceManager.carServices.GetCarDetailsAsync();
            return Ok(ApiResponse<CarDetailsDto>.SuccessResponse(Car, "Has a Car"));
        }


        [HttpPost]
        public async Task<IActionResult> AddNewCar([FromBody] CreateCarDto car)
        {
            if (car == null)
            {
                return BadRequest(ApiResponse<CreateCarDto>.FailResponse("Car data is null"));
            }
            await serviceManager.carServices.AddNewCar(car);
            return Ok(ApiResponse<CreateCarDto>.SuccessResponse(car, "Car added successfully"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCar()
        {
            await serviceManager.carServices.DeleteCar();
            return Ok(ApiResponse<string>.SuccessResponse("Car deleted successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> EditCarKm( int newCarKm)
        {
            if (newCarKm <= 0)
            {
                return BadRequest(ApiResponse<Car>.FailResponse("Invalid km value"));
            }


            await serviceManager.carServices.EditCarKm(newCarKm);
            return Ok(ApiResponse<Car>.SuccessResponse(null, "Car km updated successfully"));
        }

    }
}
