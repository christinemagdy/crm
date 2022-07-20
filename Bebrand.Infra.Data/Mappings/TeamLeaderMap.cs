using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Mappings
{
    public class TeamLeaderMap : IEntityTypeConfiguration<TeamLeader>
    {
        public void Configure(EntityTypeBuilder<TeamLeader> builder)
        {
            builder.ToTable("TeamLeaders").HasMany(x => x.TeamMembers).WithOne(x => x.TeamLeader).HasForeignKey(x => x.TeamLeaderId);
        }
    }
}
