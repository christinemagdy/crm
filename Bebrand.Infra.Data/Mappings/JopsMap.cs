using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Mappings
{

    public class JopsMap : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {

        }
    }
}
