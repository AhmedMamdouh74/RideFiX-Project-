using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using Services.HelperMethods;
using SharedData.DTOs.ActivityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class ActivityReportService : IActivityReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActivityReportResponseDTO> GetLastActivitiesAsync(int hoursBack = 12)
        {
            var fromDate = DateTime.UtcNow.AddHours(-hoursBack);
            var activities = new List<ActivityReportDTO>();

           
            var emergencyRequestRepo = _unitOfWork.GetRepository<EmergencyRequest, int>();
            var emergencyRequests = await emergencyRequestRepo.GetAllAsync();
            var emergencyRequestActivities = emergencyRequests
                .Where(er => er.TimeStamp >= fromDate)
                .OrderByDescending(er => er.TimeStamp)
                .Select(er => new ActivityReportDTO
                {
                    ActivityType = er.IsCompleted ? "Completed" : "Created",
                    Description = er.IsCompleted ? 
                        $"تم إكمال طلب خدمة طوارئ" : 
                        $"تم إنشاء طلب خدمة طوارئ جديد",
                    Timestamp = er.TimeStamp,
                    EntityType = "EmergencyRequest",
                    EntityId = er.Id
                }).FirstOrDefault();

            
            var emergencyRequestUpdates = emergencyRequests
                .Where(er => er.EndTimeStamp >= fromDate && er.IsCompleted)
                .OrderByDescending(er => er.EndTimeStamp)
                .Select(er => new ActivityReportDTO
                {
                    ActivityType = "Updated",
                    Description = $"تم تحديث حالة طلب خدمة طوارئ",
                    Timestamp = er.EndTimeStamp.Value,
                    EntityType = "EmergencyRequest",
                    EntityId = er.Id
                }).FirstOrDefault();

      
            var maintenanceRepo = _unitOfWork.GetRepository<CarMaintenanceRecord, int>();
            var maintenanceRecords = await maintenanceRepo.GetAllAsync();
            var maintenanceActivities = maintenanceRecords
                .Where(cmr => cmr.PerformedAt >= fromDate)
                .OrderByDescending(cmr => cmr.PerformedAt)
                .Select(cmr => new ActivityReportDTO
                {
                    ActivityType = "Completed",
                    Description = $"تم إكمال طلب صيانة",
                    Timestamp = cmr.PerformedAt,
                    EntityType = "CarMaintenanceRecord",
                    EntityId = cmr.Id
                }).FirstOrDefault();

           
            var reviewRepo = _unitOfWork.GetRepository<Review, int>();
            var reviews = await reviewRepo.GetAllAsync();
            var reviewActivities = reviews
                .Where(r => r.DateTime >= fromDate)
                .OrderByDescending(r => r.DateTime)
                .Select(r => new ActivityReportDTO
                {
                    ActivityType = "Created",
                    Description = $"تم إضافة تقييم جديد",
                    Timestamp = r.DateTime,
                    EntityType = "Review",
                    EntityId = r.Id
                }).FirstOrDefault();

           
            var chatSessionRepo = _unitOfWork.GetRepository<ChatSession, int>();
            var chatSessions = await chatSessionRepo.GetAllAsync();
            var chatSessionActivities = chatSessions
                .Where(cs => cs.StartAt >= fromDate)
                .OrderByDescending(cs => cs.StartAt)           
                .Select(cs => new ActivityReportDTO
                {
                    ActivityType = "Created",
                    Description = $"تم إنشاء جلسة محادثة جديدة",
                    Timestamp = cs.StartAt,
                    EntityType = "ChatSession",
                    EntityId = cs.Id
                }).FirstOrDefault();

            
            activities.Add(emergencyRequestActivities);
            activities.Add(emergencyRequestUpdates);
            activities.Add(maintenanceActivities);
            activities.Add(reviewActivities);
            activities.Add(chatSessionActivities);

           
            var orderedActivities = activities
                .OrderByDescending(a => a.Timestamp)
                .Take(1)
                .ToList();

           
            foreach (var activity in orderedActivities)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            return new ActivityReportResponseDTO
            {
                Activities = orderedActivities,
                TotalCount = orderedActivities.Count,
                ReportGeneratedAt = DateTime.UtcNow
            };
                }

        public async Task<ActivityReportResponseDTO> GetActivitiesByTypeAsync(string activityType, int hoursBack = 12)
        {
            var allActivities = await GetLastActivitiesAsync(hoursBack);
            var filteredActivities = allActivities.Activities
                .Where(a => a.ActivityType.Equals(activityType, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new ActivityReportResponseDTO
            {
                Activities = filteredActivities,
                TotalCount = filteredActivities.Count,
                ReportGeneratedAt = DateTime.UtcNow
            };
        }

        public async Task<ActivityReportResponseDTO> GetActivitiesByEntityTypeAsync(string entityType, int hoursBack = 12)
        {
            var allActivities = await GetLastActivitiesAsync(hoursBack);
            var filteredActivities = allActivities.Activities
                .Where(a => a.EntityType.Equals(entityType, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new ActivityReportResponseDTO
            {
                Activities = filteredActivities,
                TotalCount = filteredActivities.Count,
                ReportGeneratedAt = DateTime.UtcNow
            };
        }

        public async Task<CategorizedActivityReportDTO> GetCategorizedActivitiesAsync(int hoursBack = 12)
        {
            var fromDate = DateTime.UtcNow.AddHours(-hoursBack);
            var categorizedActivities = new CategorizedActivityReportDTO();

            // Get Emergency Requests activities
            var emergencyRequestRepo = _unitOfWork.GetRepository<EmergencyRequest, int>();
            var emergencyRequests = await emergencyRequestRepo.GetAllAsync();
            var emergencyRequestActivities = emergencyRequests
                .Where(er => er.TimeStamp >= fromDate)
                .OrderByDescending(er => er.TimeStamp)
                .Take(1)
                .Select(er => new ActivityReportDTO
                {
                    ActivityType = er.IsCompleted ? "Completed" : "Created",
                    Description = er.IsCompleted ? 
                        $"تم إكمال طلب خدمة طوارئ" : 
                        $"تم إنشاء طلب خدمة طوارئ جديد",
                    Timestamp = er.TimeStamp,
                    EntityType = "EmergencyRequest",
                    EntityId = er.Id
                })
                .ToList();

            
            var emergencyRequestUpdates = emergencyRequests
                .Where(er => er.EndTimeStamp >= fromDate && er.IsCompleted)
                .OrderByDescending(er => er.EndTimeStamp)
                .Take(1)
                .Select(er => new ActivityReportDTO
                {
                    ActivityType = "Updated",
                    Description = $"تم تحديث حالة طلب خدمة طوارئ",
                    Timestamp = er.EndTimeStamp.Value,
                    EntityType = "EmergencyRequest",
                    EntityId = er.Id
                })
                .ToList();

           
            categorizedActivities.EmergencyRequests.AddRange(emergencyRequestActivities);
            categorizedActivities.EmergencyRequests.AddRange(emergencyRequestUpdates);

         
            var maintenanceRepo = _unitOfWork.GetRepository<CarMaintenanceRecord, int>();
            var maintenanceRecords = await maintenanceRepo.GetAllAsync();
            var maintenanceActivities = maintenanceRecords
                .Where(cmr => cmr.PerformedAt >= fromDate)
                .OrderByDescending(cmr => cmr.PerformedAt)
                .Take(1)
                .Select(cmr => new ActivityReportDTO
                {
                    ActivityType = "Completed",
                    Description = $"تم إكمال طلب صيانة",
                    Timestamp = cmr.PerformedAt,
                    EntityType = "CarMaintenanceRecord",
                    EntityId = cmr.Id
                })
                .ToList();

            categorizedActivities.CarMaintenanceRecords.AddRange(maintenanceActivities);

         
            var reviewRepo = _unitOfWork.GetRepository<Review, int>();
            var reviews = await reviewRepo.GetAllAsync();
            var reviewActivities = reviews
                .Where(r => r.DateTime >= fromDate)
                .OrderByDescending(r => r.DateTime)
                .Take(1)
                .Select(r => new ActivityReportDTO
                {
                    ActivityType = "Created",
                    Description = $"تم إضافة تقييم جديد",
                    Timestamp = r.DateTime,
                    EntityType = "Review",
                    EntityId = r.Id
                })
                .ToList();

            categorizedActivities.Reviews.AddRange(reviewActivities);

      
            var chatSessionRepo = _unitOfWork.GetRepository<ChatSession, int>();
            var chatSessions = await chatSessionRepo.GetAllAsync();
            var chatSessionActivities = chatSessions
                .Where(cs => cs.StartAt >= fromDate)
                .OrderByDescending(cs => cs.StartAt)
                .Take(1)
                .Select(cs => new ActivityReportDTO
                {
                    ActivityType = "Created",
                    Description = $"تم إنشاء جلسة محادثة جديدة",
                    Timestamp = cs.StartAt,
                    EntityType = "ChatSession",
                    EntityId = cs.Id
                })
                .ToList();

            categorizedActivities.ChatSessions.AddRange(chatSessionActivities);

         
            foreach (var activity in categorizedActivities.EmergencyRequests)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            foreach (var activity in categorizedActivities.CarMaintenanceRecords)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            foreach (var activity in categorizedActivities.UserRegistrations)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            foreach (var activity in categorizedActivities.Reviews)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            foreach (var activity in categorizedActivities.ChatSessions)
            {
                activity.TimeAgo = TimeHelper.CalculateTimeAgoInArabic(activity.Timestamp);
            }

            categorizedActivities.TotalCount = categorizedActivities.EmergencyRequests.Count +
                                              categorizedActivities.CarMaintenanceRecords.Count +
                                              categorizedActivities.UserRegistrations.Count +
                                              categorizedActivities.Reviews.Count +
                                              categorizedActivities.ChatSessions.Count;

            return categorizedActivities;
        }
    }
}
