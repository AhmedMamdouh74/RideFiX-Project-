using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.Admin.TechnicianCategory
{
    public class TechnicianCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
