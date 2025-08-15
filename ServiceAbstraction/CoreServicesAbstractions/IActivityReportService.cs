using SharedData.DTOs.ActivityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IActivityReportService
    {
        Task<ActivityReportResponseDTO> GetLastActivitiesAsync(int hoursBack = 12);
        Task<ActivityReportResponseDTO> GetActivitiesByTypeAsync(string activityType, int hoursBack = 12);
        Task<ActivityReportResponseDTO> GetActivitiesByEntityTypeAsync(string entityType, int hoursBack = 12);
        Task<CategorizedActivityReportDTO> GetCategorizedActivitiesAsync(int hoursBack = 12);
    }
}
