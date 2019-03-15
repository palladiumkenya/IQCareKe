using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class IndicatorResultMapping : IEntityTypeConfiguration<IndicatorResult>
    {
        public void Configure(EntityTypeBuilder<IndicatorResult> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.ReportingPeriodId).HasColumnName("FormReportingPId");
            builder.Property(x => x.DateUpdated).HasColumnName("UpdateDate");

            builder.ToTable("IndicatorResults");
        }
    }
}
