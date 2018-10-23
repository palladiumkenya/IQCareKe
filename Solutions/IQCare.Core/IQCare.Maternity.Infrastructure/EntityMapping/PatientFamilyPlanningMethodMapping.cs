using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientFamilyPlanningMethodMapping : IEntityTypeConfiguration<PatientFamilyPlanningMethod>
    {
        public void Configure(EntityTypeBuilder<PatientFamilyPlanningMethod> builder)
        {
            builder.ToTable(nameof(PatientFamilyPlanningMethod));
        }
    }
}
