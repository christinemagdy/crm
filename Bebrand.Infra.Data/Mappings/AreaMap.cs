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
    public class AreaMap : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {

            builder.ToTable("Area").HasMany(x => x.Clients).WithOne(x => x.Area).HasForeignKey(x => x.AriaId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
