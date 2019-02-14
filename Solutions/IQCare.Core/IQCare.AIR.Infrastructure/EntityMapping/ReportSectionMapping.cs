using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class ReportSectionMapping : IEntityTypeConfiguration<ReportSection>
    {
        public void Configure(EntityTypeBuilder<ReportSection> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.DateUpdated).HasColumnName("UpdateDate");
            builder.Property(x => x.Name).HasColumnName("SectionName");
            builder.Property(x => x.ReportigFormId).HasColumnName("FormId");

            builder.ToTable("Section");
        }
    }
}
