using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class HTSEncounterMapping : IEntityTypeConfiguration<HtsEncounter>
    {
        public void Configure(EntityTypeBuilder<HtsEncounter> builder)
        {
            builder.ToTable("HtsEncounter")
                .HasKey(c => c.Id);
        }
    }
}
