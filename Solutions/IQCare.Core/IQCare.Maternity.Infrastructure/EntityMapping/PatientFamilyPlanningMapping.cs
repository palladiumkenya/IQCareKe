using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientFamilyPlanningMapping : IEntityTypeConfiguration<PatientFamilyPlanningMapping>
    {
        public void Configure(EntityTypeBuilder<PatientFamilyPlanningMapping> builder)
        {
            builder.ToTable(nameof(PatientFamilyPlanningMapping));
        }
    }
}
