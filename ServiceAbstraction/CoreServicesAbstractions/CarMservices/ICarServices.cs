using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.DTOs.Car;

namespace Service.CoreServices.CarMservices
{
    public interface ICarServices
    {
        public Task<CarDetailsDto> GetCarDetailsAsync();
        public Task AddNewCar(CreateCarDto car);
    }
}
