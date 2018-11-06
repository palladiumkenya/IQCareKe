using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientScreeningMapping : IEntityTypeConfiguration<PatientScreening>
    {
        public void Configure(EntityTypeBuilder<PatientScreening> builder)
        {
            builder.ToTable(nameof(PatientScreening)).HasKey(x => x.Id);
        }
    }
}