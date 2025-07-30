
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
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.Enums;
using Microsoft.EntityFrameworkCore;
using Presentation.Hubs;

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
                              .AllowAnyHeader()
                              .SetIsOriginAllowed(origin => true);
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSignalR(); // Add SignalR services


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

            //notification hub configuration
            app.MapHub<NotificationHub>("/notificationhub");
            app.MapHub<ChatHub>("/chathub");



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

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // بيانات الفنيين مع الـ ApplicationId
                var technicians = new List<(int TechnicianId, Guid ApplicationId)>
    {
        (3, Guid.Parse("24179d1b-4f50-482b-b383-9ac5e80cf0fd")),
        (7, Guid.Parse("23ea8c83-56ca-4156-877e-a22650d9a966")),
        (9, Guid.Parse("245246d4-9521-4d8d-9c42-c41349a7a035")),
        (6, Guid.Parse("5f685ed9-38af-4705-802c-6266caf3155f"))
    };

                var carOwnerId = 1;
                var carOwnerAppId = Guid.Parse("28f9a14d-f846-427e-836f-760c87577cc7");

                foreach (var tech in technicians)
                {
                    var start = DateTime.UtcNow.AddMinutes(-15);
                    var end = start.AddMinutes(5);

                    var session = new ChatSession
                    {
                        StartAt = start,
                        EndAt = end,
                        IsClosed = true,
                        CarOwnerId = carOwnerId,
                        TechnicianId = tech.TechnicianId
                    };

                    context.chatSessions.Add(session);
                    await context.SaveChangesAsync();

                    var message1 = new Message
                    {
                        Text = $"مساء الخير، محتاجة حد يشوف العربية ضروري 🌟",
                        SentAt = start.AddMinutes(1),
                        IsSeen = true,
                        ChatSessionId = session.Id,
                        ApplicationId = carOwnerAppId.ToString()
                    };

                    var message2 = new Message
                    {
                        Text = $"أنا تحت أمرك يا فندم، فين مكانك؟",
                        SentAt = start.AddMinutes(2),
                        IsSeen = true,
                        ChatSessionId = session.Id,
                        ApplicationId = tech.ApplicationId.ToString()
                    };

                    context.messages.AddRange(message1, message2);
                    await context.SaveChangesAsync();
                }
            }




            app.Run();
        }
    }
}
