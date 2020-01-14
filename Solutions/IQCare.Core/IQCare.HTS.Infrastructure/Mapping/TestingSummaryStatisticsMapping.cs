using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class TestingSummaryStatisticsMapping : IEntityTypeConfiguration<TestingSummaryStatistics>
    {
        public void Configure(EntityTypeBuilder<TestingSummaryStatistics> builder)
        {
            builder.ToTable("TestingSummaryStatistics")
                .HasKey(c => c.Id);
        }
    }
}
