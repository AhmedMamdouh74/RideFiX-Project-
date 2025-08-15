using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation.CategorySpecification
{
    public class CategoryNotDeletedSpecification : Specification<TCategory, int>
    {
        public CategoryNotDeletedSpecification()
      : base(c => c.IsDeleted == false) 
        {
        }
    }
}
