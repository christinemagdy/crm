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
    public class VacanciesMailMap : IEntityTypeConfiguration<VacanciesMail>
    {
        public void Configure(EntityTypeBuilder<VacanciesMail> builder)
        {
            builder.HasOne(x => x.Jobs).WithMany(x => x.vacanciesMails).HasForeignKey(x => x.JobId);
            builder.Property(x => x.Attachement).HasMaxLength(8000);
        }
    }
}
