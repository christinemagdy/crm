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
     public class HrMap : IEntityTypeConfiguration<Hr>
    {
        public void Configure(EntityTypeBuilder<Hr> builder)
        {
            builder.ToTable("Hr");
        }
    }
}
