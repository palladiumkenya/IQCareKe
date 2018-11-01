using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientExaminationMapping : IEntityTypeConfiguration<PhysicalExamination>
    {
        public void Configure(EntityTypeBuilder<PhysicalExamination> builder)
        {
            builder.ToTable(nameof(PhysicalExamination)).HasKey(x => x.Id);
        }
    }
}
