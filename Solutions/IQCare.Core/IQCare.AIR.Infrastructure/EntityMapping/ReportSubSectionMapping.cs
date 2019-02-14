using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class ReportSubSectionMapping : IEntityTypeConfiguration<ReportSubSection>
    {
        public void Configure(EntityTypeBuilder<ReportSubSection> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.DateUpdated).HasColumnName("UpdateDate");
            builder.Property(x => x.Name).HasColumnName("SubSectionName");
            builder.Property(x => x.ReportSectionId).HasColumnName("SectionId");

            builder.ToTable("Section");
        }
    }
}
