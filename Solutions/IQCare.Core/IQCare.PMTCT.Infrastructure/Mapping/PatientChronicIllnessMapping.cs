using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientChronicIllnessMapping : IEntityTypeConfiguration<PatientChronicIllness>
    {
        public void Configure(EntityTypeBuilder<PatientChronicIllness> builder)
        {
            builder.ToTable("PatientChronicIllness")
                .HasKey(c => c.Id);
        }
    }
}
