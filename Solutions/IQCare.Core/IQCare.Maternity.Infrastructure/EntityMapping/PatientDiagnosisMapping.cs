using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientDiagnosisMapping : IEntityTypeConfiguration<PatientDiagnosis>
    {
        public void Configure(EntityTypeBuilder<PatientDiagnosis> builder)
        {
            builder.ToTable(nameof(PatientDiagnosis)).Property(x => x.CreatedBy).HasColumnName("CreateBy");
            builder.ToTable(nameof(PatientDiagnosis)).Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.ToTable(nameof(PatientDiagnosis));
        }
    }
}
