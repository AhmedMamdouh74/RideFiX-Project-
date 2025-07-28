
using System.Text;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Presistence;
using Presistence.Data;
using Domain.Contracts;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistence;
using Presistence.Data;
using Domain.Contracts;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistence;
using Presistence.Data;
using RideFix.CustomMiddlewares;
using Services;

namespace RideFix
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Services Configurations
            builder.Services.AddPresistenceConfig(builder.Configuration); // Custom extension method to add persistence layer configurations
            builder.Services.AddServiceConfig();// Custom extension method to add service layer configurations
            #endregion

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });


            ; builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                            {
                                var config = builder.Configuration;
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = config["JWT:Issuer"],
                                    ValidAudience = config["JWT:Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
                                };
                            });


            #region Invalid Model State Response Factory Configuration
            builder.Services.Configure<ApiBehaviorOptions>(ApiBehaviorOptions =>
            {
                ApiBehaviorOptions.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ErrorModels.ValidationError
                        {
                            Key = e.Key,
                            Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                        }).ToArray();
                    var Error = new ErrorModels.ValidationErrorToReturn
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(Error);
                };
            });
            #endregion


            var app = builder.Build();
            app.UseCors("AllowAll");


            #region Exception Handler Middleware Configuration
            app.UseMiddleware<CustomExceptionMiddleware>();
            #endregion

            #region Data Seeding Configuration
            using (var scope = app.Services.CreateScope())
            {
                var dataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await dataSeeding.SeedCategories();
                await dataSeeding.SeedIdentityDataAsync();
                await dataSeeding.SeedDataAsync();
            }
            #endregion


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
         //   }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
