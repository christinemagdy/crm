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
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client").HasOne(x => x.Area).WithMany(x => x.Clients).HasForeignKey(x => x.AriaId);
            builder.ToTable("Client").Property(x => x.AriaId).HasDefaultValue(Guid.Empty);
            builder.Property(x => x.Birthday).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Name_of_business).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.Number).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Nameofcontact).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Position).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Completeaddress).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Field).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Facebooklink).HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.Instagramlink).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Website).HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Lastfeedback).HasColumnType("nvarchar(MAX)");


        }
    }
}
