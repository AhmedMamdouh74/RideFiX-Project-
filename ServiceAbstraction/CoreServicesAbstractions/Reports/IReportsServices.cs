using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.DTOs.ReportDtos;

namespace ServiceAbstraction.CoreServicesAbstractions.Reports
{
    public interface IReportsServices
    {
        public Task AddReportAsync(CreateReportDto reportDto);
    }
}
