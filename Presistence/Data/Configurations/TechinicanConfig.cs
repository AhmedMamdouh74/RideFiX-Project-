using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data.Configurations
{
    public class TechinicanConfig : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasKey(te => te.Id);

            builder.HasOne(te => te.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(te => te.ApplicationUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(te => te.EmergencyRequestTechnicians)
                   .WithOne(ert => ert.Technician)
                   .HasForeignKey(ert => ert.TechnicianId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(te => te.EmergencyRequests)
                .WithOne(e => e.Technician)
                .HasForeignKey(e => e.TechnicianId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
