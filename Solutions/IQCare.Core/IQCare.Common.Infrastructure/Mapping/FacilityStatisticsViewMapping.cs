using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class FacilityStatisticsViewMapping : IEntityTypeConfiguration<FacilityStatisticsView>
    {
        public void Configure(EntityTypeBuilder<FacilityStatisticsView> builder)
        {
            builder.ToTable("facilityStatisticsView").HasKey(c => c.Id);
        }
    }
}
