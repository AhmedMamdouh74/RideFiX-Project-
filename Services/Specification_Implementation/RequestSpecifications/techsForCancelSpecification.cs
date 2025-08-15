using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;

namespace Service.Specification_Implementation.RequestSpecifications
{
    internal class techsForCancelSpecification : RequestSpecification
    {
        public techsForCancelSpecification(int reqId , int techId) : 
            base(x => x.TechnicianId == techId && x.EmergencyRequestId == reqId )
        {
        }
    }
}
