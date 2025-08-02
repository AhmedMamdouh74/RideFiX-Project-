using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.DTOs.RequestsDTOs;

namespace Service.Specification_Implementation.CarOwnerSpecifications
{
    public class CarOwnerUserPinSpecification : Specification<CarOwner, int>
    {
        public CarOwnerUserPinSpecification(RealRequestDTO request)
            : base(co => co.Id == request.CarOwnerId)
        {
            AddInclude(co => co.ApplicationUser);
        }
    }
}
