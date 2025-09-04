using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation.CarSpecification
{
    public class CarByOwnerIdSpecification : Specification<Car, int>
    {
        public CarByOwnerIdSpecification(int id) : base(C => C.OwnerId == id) 
        {
        }
    }
}
