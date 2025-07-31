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
    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rate).IsRequired();
            builder.Property(r => r.Comment).HasMaxLength(1000);
            builder.Property(r => r.DateTime).IsRequired();

            builder.HasOne(r => r.CarOwner)
                   .WithMany(er=>er.Reviews)
                   .HasForeignKey(r => r.CarOwnerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Technician)
                   .WithMany(r=>r.reviews)
                   .HasForeignKey(r => r.TechnicianId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(r => r.EmergencyRequest)
                     .WithOne(er => er.Review)
                     .HasForeignKey<Review>(r => r.EmergencyRequestId)
                     .OnDelete(DeleteBehavior.NoAction)
                     .IsRequired(false);
        }

    }
}

