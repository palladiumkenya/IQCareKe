using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
   public  class SubCountyLookupMapping : IEntityTypeConfiguration<SubCountyLookup>
    {
        public void Configure(EntityTypeBuilder<SubCountyLookup> builder)
        {
            builder.ToTable("SubCountyView")
             .HasKey(c => c.SubCountyId);
        }
    }
}
