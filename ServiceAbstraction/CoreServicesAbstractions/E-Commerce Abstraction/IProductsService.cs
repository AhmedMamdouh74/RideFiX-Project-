using Domain.Entities.e_Commerce;
using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.E_Commerce_Abstraction
{
    public interface IProductsService 
    {
        public Task<List<ProductBreifDTO>> FilterProductsAsync(
            int? pageNumber,
            int? itemPerPage,
            decimal? maxPrice = null,
            int? categoryId = null);

        public Task<CartItemDto> GetProductByIdAsync(int productId);
    }
}
