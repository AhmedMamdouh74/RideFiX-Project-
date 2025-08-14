using AutoMapper;
using Domain.Entities.e_Commerce;
using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile.E_CommerceMapping
{
    public class ProductCategoryMapConfig : Profile
    {
        public ProductCategoryMapConfig()
        {
            CreateMap<Category, ProductCategoryDto>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Count))
                .ReverseMap();
        }
    }
}
