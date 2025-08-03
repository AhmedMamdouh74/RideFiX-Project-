using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.TechnicianSpecifications
{
    public class TechnicianWithReviewsAndCatergorySpecification : Specification<Technician, int>
    {
        public TechnicianWithReviewsAndCatergorySpecification(int id) :
            base(t => t.Id == id )
        {
            AddInclude(technician => technician.ApplicationUser);
            AddInclude(technician => technician.reviews);
            AddInclude(technician => technician.TCategories);
        }
    }
}
