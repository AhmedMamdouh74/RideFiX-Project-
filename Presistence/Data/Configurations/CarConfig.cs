using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Presistence.Data.Configurations
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasOne(C => C.Owner).
                WithOne(O => O.Car).
                HasForeignKey<Car>(C => C.OwnerId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(c => c.TotalMaintenanceCost)
                .HasColumnType("decimal(18,2)");

        }
    }
}
