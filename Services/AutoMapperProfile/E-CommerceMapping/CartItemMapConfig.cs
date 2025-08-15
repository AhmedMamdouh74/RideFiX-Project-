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
    public class CartItemMapConfig : Profile
    {
        public CartItemMapConfig()
        {
            CreateMap<Product, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
