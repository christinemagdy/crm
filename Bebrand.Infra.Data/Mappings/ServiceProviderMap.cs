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
    public class ServiceProviderMap : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.HasOne(x => x.Client).WithMany(x => x.ServiceProviders).HasForeignKey(x => x.ClientID);
            builder.HasOne(x => x.Service).WithMany(x => x.ServiceProviders).HasForeignKey(x => x.ServiceId);
        }
    }
}
