using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.MaintananceDTOs
{
    public class GetMaintananceByID : Specification<CarMaintenanceRecord, int>
    {
        public GetMaintananceByID(int carId, int maintananceId) :
            base(s => s.MaintenanceTypeId == maintananceId && s.CarId == carId)
        {
        }
    }
}
