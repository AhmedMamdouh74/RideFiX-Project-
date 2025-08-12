using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation.MaintenanceSpecification
{
    internal class MaintenanceSummarySpecification : Specification<MaintenanceTypes, int>
    {
        public MaintenanceSummarySpecification(int CarId) : base(null!)
        {
            AddInclude(m => m.CarMaintenanceRecords
            .Where(r => r.CarId == CarId)
            .OrderByDescending(r => r.PerformedAt));
        }
    }
}
