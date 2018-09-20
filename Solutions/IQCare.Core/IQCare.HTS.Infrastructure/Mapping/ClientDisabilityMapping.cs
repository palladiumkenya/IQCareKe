using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class ClientDisabilityMapping : IEntityTypeConfiguration<ClientDisability>
    {
        public void Configure(EntityTypeBuilder<ClientDisability> builder)
        {
            builder.ToTable("ClientDisability")
                .HasKey(c => c.Id);
        }
    }
}
