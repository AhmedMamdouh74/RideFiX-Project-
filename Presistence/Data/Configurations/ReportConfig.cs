using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Domain.Entities.Reporting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasOne(r => r.ReportedUser)
                .WithMany(u => u.Reported)
                .HasForeignKey(r => r.ReportedUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(r => r.ReportingUser)
                .WithMany(builder => builder.Reporting)
                .HasForeignKey(r => r.ReportingUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
