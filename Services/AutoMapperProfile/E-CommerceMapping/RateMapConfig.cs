using AutoMapper;
using Domain.Entities.e_Commerce;
using Hangfire.MemoryStorage.Dto;
using SharedData.DTOs.E_CommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile.E_CommerceMapping
{
    public class RateMapConfig : Profile
    {
        public RateMapConfig()
        {
            CreateMap<Rate, RateDTO>()
                .ReverseMap();
            CreateMap<Product, ProductWithRatesDTO>()
                //.ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
                //.ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
                .ForMember(dest => dest.ProductRates, opt => opt.MapFrom(src => src.ProductRates))
                .ReverseMap();
        }
    }
}
