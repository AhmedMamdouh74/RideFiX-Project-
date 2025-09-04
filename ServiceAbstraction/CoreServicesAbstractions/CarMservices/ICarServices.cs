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
        public Task DeleteCar();
        public Task<int> GetCarIdByOwnerId(int ownerId);
        public Task SetCarStats(DateTime date, decimal cost, int CarId);
        public Task EditCarKm(int AvgKmPerMonth);
    }
}
