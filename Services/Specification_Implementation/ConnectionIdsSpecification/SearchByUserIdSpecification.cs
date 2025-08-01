using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.ConnectionIdsSpecification
{
    public class SearchByUserIdSpecification : ConnectionIdSpecification
    {
        public SearchByUserIdSpecification(string Id) : base(s => s.ApplicationUserId == Id)
        {
        }
    }
}
