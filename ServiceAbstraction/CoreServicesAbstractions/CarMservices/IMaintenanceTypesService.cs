using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.DTOs.MTypesDtos;

namespace ServiceAbstraction.CoreServicesAbstractions.CarMservices
{
    public interface IMaintenanceTypesService
    {
        public Task<MaintenanceTypeDetailsDto> GetMTypebyIdAsync(int Id);
    }
}
