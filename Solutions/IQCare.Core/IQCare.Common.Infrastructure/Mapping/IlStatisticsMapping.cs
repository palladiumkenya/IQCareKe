using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class IlStatisticsMapping : IEntityTypeConfiguration<IlStatistics>
    {
        public void Configure(EntityTypeBuilder<IlStatistics> builder)
        {
            builder.ToTable("vw_ILStatistics").HasKey(c => c.Id);
        }
    }
}
