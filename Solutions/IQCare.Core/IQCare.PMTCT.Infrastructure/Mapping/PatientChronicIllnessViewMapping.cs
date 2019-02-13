using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientChronicIllnessViewMapping : IEntityTypeConfiguration<PatientChronicIllnessView>
    {
        public void Configure(EntityTypeBuilder<PatientChronicIllnessView> builder)
        {
            builder.ToTable(nameof(PatientChronicIllnessView));
        }
    }
}
