using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class GetCurrentRequestSpecification : Specification<EmergencyRequest, int>
    {
        public GetCurrentRequestSpecification(int carOwnerId) : base(s => s.CarOwnerId == carOwnerId)
        {
        }
    }
}
