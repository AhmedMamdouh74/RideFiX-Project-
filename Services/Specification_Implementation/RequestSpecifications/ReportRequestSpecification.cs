using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class ReportRequestSpecification : Specification<EmergencyRequest, int>
    {
        public ReportRequestSpecification(int reqId) : base(x => x.Id == reqId)
        {
            AddInclude(x => x.Technician);
            AddInclude(x => x.CarOwner);
        }
    }
}
