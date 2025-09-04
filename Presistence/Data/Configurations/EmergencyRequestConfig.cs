using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    internal class EmergencyRequestConfig : IEntityTypeConfiguration<EmergencyRequest>
    {
        public void Configure(EntityTypeBuilder<EmergencyRequest> builder)
        {

            builder.Property(er => er.IsCompleted)
                   .IsRequired();

            builder.HasMany(e => e.EmergencyRequestTechnicians)
                    .WithOne(er => er.EmergencyRequests)
                    .HasForeignKey(er => er.EmergencyRequestId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(er => er.CarOwner)
                   .WithMany(er=>er.EmergencyRequests)
                   .HasForeignKey(er => er.CarOwnerId)
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.
        }
    }
}
