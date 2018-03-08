using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class HtsEncounterResultMapping : IEntityTypeConfiguration<HtsEncounterResult>
    {
        public void Configure(EntityTypeBuilder<HtsEncounterResult> builder)
        {
            builder.ToTable("HtsEncounterResult")
                .HasKey(c => c.Id);
        }
    }
}
