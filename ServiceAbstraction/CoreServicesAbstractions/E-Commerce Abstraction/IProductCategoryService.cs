using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.E_Commerce_Abstraction
{
    public interface IProductCategoryService
    {
        public Task<List<ProductCategoryDto>> GetAllProductCategoriesAsync();
    }
}
