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
    public class ProductBreifMapConfig : Profile
    {
        public ProductBreifMapConfig()
        {
            CreateMap<Product, ProductBreifDTO>().ReverseMap();
              
        }
    }
}
