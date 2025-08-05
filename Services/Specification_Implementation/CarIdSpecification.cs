using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class CarIdSpecification : Specification<Car, int>
    {
        public CarIdSpecification(int ownerId) : base(s => s.OwnerId == ownerId )
        {
        }
    }
}
