using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class LookupItemViewMapping : IEntityTypeConfiguration<LookupItemView>
    {
        public void Configure(EntityTypeBuilder<LookupItemView> builder)
        {
            builder.ToTable("LookupItemView")
                .HasKey(c => c.RowID);
        }
    }
}
   