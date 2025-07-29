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
    public class UserConnectionIdsConfig : IEntityTypeConfiguration<UserConnectionIds>
    {
        public void Configure(EntityTypeBuilder<UserConnectionIds> builder)
        {
            builder.HasKey(U => new { U.ApplicationUserId, U.ConnectionId });
        }
    }
}
