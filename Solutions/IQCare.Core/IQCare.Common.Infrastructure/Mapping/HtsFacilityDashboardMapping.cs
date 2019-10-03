using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HtsFacilityDashboardMapping : IEntityTypeConfiguration<HtsFacilityDashboard>
    {
        public void Configure(EntityTypeBuilder<HtsFacilityDashboard> builder)
        {
            builder.ToTable("HtsFacilityDashboard").HasKey(c => c.Id);
        }
    }
}
