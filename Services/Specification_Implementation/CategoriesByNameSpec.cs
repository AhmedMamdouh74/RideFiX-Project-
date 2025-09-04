using Domain.Entities.CoreEntites.EmergencyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Specification_Implementation.Emergency
{
    public class CategoriesByNameSpec : Specification<TCategory, int>
    {
        public CategoriesByNameSpec(IEnumerable<string> categoryNames)
            : base(c => categoryNames.Contains(c.Name))
        {
        }
        public CategoriesByNameSpec()
           : base(c => c.IsDeleted==false)
        {
        }

    }
}
