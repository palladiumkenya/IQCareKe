using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class ReportingPeriodMapping : IEntityTypeConfiguration<ReportingPeriod>
    {
        public void Configure(EntityTypeBuilder<ReportingPeriod> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.DateUpdated).HasColumnName("UpdateDate");
            builder.Property(x => x.ReportDate).HasColumnName("FormReportingDate");
            builder.Property(x => x.ReportingFormId).HasColumnName("FormId");

            builder.ToTable("FormReportingPeriod");
        }
    }
}
