
using Domain.Contracts;
using Domain.Contracts.ReposatoriesContract;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repositories;
using Presistence.unitofwork;



namespace Presistence
{
    public static class PresistenceLayerConfigurations
    {
        public static IServiceCollection AddPresistenceConfig(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        _configuration.GetConnectionString("DefaultConnection"),
                          sqlOptions => sqlOptions.EnableRetryOnFailure()
                    ));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IEmergencyRequestReposatory, EmergencyRequestReposatory>();
            Services.AddScoped<IConnectionIdsRepository, ConnectionIdsRepository>();
            return Services;
        }
    }
}
