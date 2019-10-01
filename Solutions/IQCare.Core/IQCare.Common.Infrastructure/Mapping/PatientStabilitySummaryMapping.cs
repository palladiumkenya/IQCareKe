using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientStabilitySummaryMapping : IEntityTypeConfiguration<PatientStabilitySummary>
    {
        public void Configure(EntityTypeBuilder<PatientStabilitySummary> builder)
        {
            builder.ToTable("PatientStabilitySummary").HasKey(c => c.Id);
        }
    }
}
