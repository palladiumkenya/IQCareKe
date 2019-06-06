using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientEncounterMapping : IEntityTypeConfiguration<PatientEncounter>
    {
        public void Configure(EntityTypeBuilder<PatientEncounter> builder)
        {
            builder.ToTable("PatientEncounter")
                .HasKey(c => c.Id);
        }
    }
}
