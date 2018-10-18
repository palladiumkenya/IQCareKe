using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class PatientLabTrackerMapping : IEntityTypeConfiguration<PatientLabTracker>
    {
        public void Configure(EntityTypeBuilder<PatientLabTracker> builder)
        {
            builder.ToTable("PatientLabTracker").HasKey(c => c.Id);
        }
    }
}
