using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class FacilityMapping : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.ToTable("mst_Facility").HasKey(c => c.FacilityID);
        }
    }
}
