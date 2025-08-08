using Microsoft.Extensions.DependencyInjection;
using Service.AutoMapperProfile;
using Service.CoreServices.Account;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using Services.AutoMapperProfile;

using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service.AutoMapperProfile;
using Service.CoreServices;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;
using Domain.Contracts.ReposatoriesContract;
using Service.CoreServices.CarMservices;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using Service.CoreServices.EmergencyReqServices;
using Service.CoreServices.ChatServices;



namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(RegisterMapping));
            Services.AddScoped<IFileService, FileService>();
            Services.AddHttpClient(); // تحضير الـ HttpClient في الـ DI
            Services.AddScoped<IFaceRecognitionService, FaceRecognitionService>(); Services.AddScoped<IAuthService, AuthService>();
            Services.AddMemoryCache();
            Services.AddScoped<IJwtService, JwtService>();

            Services.AddScoped<IRequestServices, RequstServices>();
            Services.AddScoped<ITechnicianService, TechnicianService>();
            Services.AddScoped<ITechnicianRequestEmergency, TechnicianRequestEmergency>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IReviewService, ReviewService>();
            Services.AddScoped<IUserProfileService, UserProfileService>();
            Services.AddAutoMapper(typeof(UserProfileMapConfig));
            Services.AddAutoMapper(typeof(PreRequestMapConfig));
            Services.AddScoped<ICarOwnerService, CarOwnerService>();
            Services.AddScoped<IMessegeService, MessegeService>();
            Services.AddScoped<IChatService, ChatService>();
            Services.AddScoped<IUserConnectionIdService, UserConnectionIdService>();
            Services.AddScoped<IChatSessionService , ChatSessionService>();
            Services.AddScoped<ICarServices, CarServices>();
            Services.AddScoped<ICarMaintananceService, CarMaintananceService>();
            Services.AddScoped<IMaintenanceTypesService, MaintenanceTypesService>();
            Services.AddScoped<IEmailService, EmailService>();


            return Services;
        }
    }
}
