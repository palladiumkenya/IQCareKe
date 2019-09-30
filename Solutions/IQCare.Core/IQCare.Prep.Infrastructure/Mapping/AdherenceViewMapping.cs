using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class AdherenceViewMapping : IEntityTypeConfiguration<AdherenceView>
    {
        public void Configure(EntityTypeBuilder<AdherenceView> builder)
        {
            builder.ToTable("AdherenceView")
                .HasKey(c => c.Id);
        }
    }
}

