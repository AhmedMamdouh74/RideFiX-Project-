using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation.CategorySpecification;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.EmergencyReqServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TCategoryDTO>> GetAllCategoriesAsync()
        {
            var spec = new CategoryNotDeletedSpecification();

            var categoryDTOs = await unitOfWork.GetRepository<TCategory, int>()
                .GetAllAsync(spec);
            var mappedCategories = mapper.Map<IEnumerable<TCategory>, IEnumerable<TCategoryDTO>>(categoryDTOs);
        
            return mappedCategories.ToList();
        }
    }
}
