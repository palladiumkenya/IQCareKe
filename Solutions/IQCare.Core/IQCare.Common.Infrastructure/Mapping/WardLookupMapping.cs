using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public  class WardLookupMapping :IEntityTypeConfiguration<WardLookup>
    {
        public void Configure(EntityTypeBuilder<WardLookup> builder)
        {
            builder.ToTable("WardView").HasKey(c => c.WardId);
        }
    }
}
