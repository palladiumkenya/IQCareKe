using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;


namespace IQCare.Pharm.Infrastructure.Mapping
{
   public class ARVTreatmentTrackerMapping : IEntityTypeConfiguration<ARVTreatmentTracker>
    {
        public void Configure(EntityTypeBuilder<ARVTreatmentTracker> builder)
        {
            builder.ToTable("ARVTreatmentTracker").HasKey(c => c.Id);
        }
    }
}