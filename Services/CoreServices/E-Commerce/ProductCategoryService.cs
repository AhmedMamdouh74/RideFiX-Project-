using AutoMapper;
using Domain.Contracts;
using Domain.Entities.e_Commerce;
using Service.Specification_Implementation.E_CommerceSpecifications;
using ServiceAbstraction.CoreServicesAbstractions.E_Commerce_Abstraction;
using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.E_Commerce
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<ProductCategoryDto>> GetAllProductCategoriesAsync()
        {
            var spec = new ProductCategoriesSpecification();
            var productCategories = await unitOfWork.GetRepository<Category, int>().GetAllAsync(spec);
            if (productCategories == null || !productCategories.Any())
            {
                return new List<ProductCategoryDto>();
            }
            var productCategoriesDto = mapper.Map<List<ProductCategoryDto>>(productCategories);
            return productCategoriesDto;
        }
    }
}
