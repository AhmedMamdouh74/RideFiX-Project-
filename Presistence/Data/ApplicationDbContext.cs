using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Configurations;


namespace Presistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefference).Assembly);
        }
      
        public DbSet<Technician> technicians { get; set; }
        public DbSet<CarOwner> carOwners { get; set; }
        public DbSet<TCategory> categories { get; set; }

        public DbSet<ChatSession> chatSessions { get; set; }
        public DbSet<Message> messages { get; set; }

        public DbSet<EmergencyRequest> emergencyRequests { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<EmergencyRequestTechnicians> EmergencyRequestTechnicians { get; set; }
        public DbSet<UserConnectionIds> UserConnectionIds { get; set; }
    }
}
