using SharedData.DTOs.CarMaintananceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.CarMservices
{
    public interface ICarMaintananceService
    {
        public Task AddMaintananceRecord(CarMaintananceAllDTO carMaintananceAllDTO);
        //public Task<DateOnly> DetermindNextDate(string maintananceType, DateOnly lastMaintananceDate);
        public Task<List<MaintenanceSummaryDTO>> GetMaintenanceSummary();
        public Task<List<MaintananceHistory>> GetAllMaintananceHistoryByID( int maintananceId);

    }
}
