using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Domain.Entities.Reporting;
using SharedData.DTOs.Car;
using SharedData.DTOs.ReportDtos;

namespace Service.AutoMapperProfile.ReportMapping
{
    internal class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<CreateReportDto, Report>().ReverseMap();
        }
    }
}
