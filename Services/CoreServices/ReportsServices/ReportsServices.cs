using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.Reporting;
using Microsoft.AspNetCore.Http;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Exception_Implementation.BadRequestExceptions;
using Service.Specification_Implementation.RequestSpecifications;
using ServiceAbstraction.CoreServicesAbstractions.Reports;
using SharedData.DTOs.ReportDtos;

namespace Service.CoreServices.ReportsServices
{
    public class ReportsServices : IReportsServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ReportsServices(IUnitOfWork _unitOfWork, IMapper _mapper, IHttpContextAccessor httpContextAccessor)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task AddReportAsync(CreateReportDto reportDto)
        {
            #region Report initialization
            var reportRepository = unitOfWork.GetRepository<Report, int>();
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new ArgumentException("no authorize");
            }
            var userId = user.User.Claims.FirstOrDefault(s => s.Type == "userId")?.Value;
            var role = user.User.Claims.FirstOrDefault(s => s.Type == "Role")?.Value;
            reportDto.ReportingUserId = userId;

            var spec = new ReportRequestSpecification(reportDto.RequestId);
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            if (role == "CarOwner")
            {
                reportDto.ReportedUserId = request.Technician.ApplicationUserId;
            }
            else if (role == "Technician")
            {
                reportDto.ReportedUserId = request.CarOwner.ApplicationUserId;
            }
            reportDto.CreatedAt = DateTime.Now;
            #endregion

            var report = mapper.Map<Report>(reportDto);
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report), "Report cannot be null");
            }
            await unitOfWork.GetRepository<Report, int>().AddAsync(report);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
