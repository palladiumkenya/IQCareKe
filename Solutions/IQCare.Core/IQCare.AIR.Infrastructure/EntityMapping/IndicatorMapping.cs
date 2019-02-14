using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class IndicatorMapping : IEntityTypeConfiguration<Indicator>
    {
        public void Configure(EntityTypeBuilder<Indicator> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.Name).HasColumnName("IndicatorName");
            builder.Property(x => x.ReportSubSectionId).HasColumnName("SubSectionId");
            builder.Property(x => x.DateUpdated).HasColumnName("UpdateDate");

            builder.ToTable(nameof(Indicator));
        }
    }
}
