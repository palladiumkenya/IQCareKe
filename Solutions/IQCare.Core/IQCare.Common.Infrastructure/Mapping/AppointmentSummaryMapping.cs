using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AppointmentSummaryMapping : IEntityTypeConfiguration<AppointmentSummary>
    {
        public void Configure(EntityTypeBuilder<AppointmentSummary> builder)
        {
            builder.ToTable("AppointmentSummaryView").HasKey(c => c.Id);
        }
    }
}
