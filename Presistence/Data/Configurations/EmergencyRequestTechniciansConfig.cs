using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
{
    public class EmergencyRequestTechniciansConfig : IEntityTypeConfiguration<EmergencyRequestTechnicians>
    {
        public void Configure(EntityTypeBuilder<EmergencyRequestTechnicians> builder)
        {
            builder.HasKey(ert => new { ert.EmergencyRequestId, ert.TechnicianId });
          
        }
    }
}
