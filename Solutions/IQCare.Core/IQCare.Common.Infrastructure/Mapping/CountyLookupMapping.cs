using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
  public  class CountyLookupMapping : IEntityTypeConfiguration<CountyLookup>
    {
        public void Configure(EntityTypeBuilder<CountyLookup> builder)
        {
            builder.ToTable("CountyView")
             .HasKey(c => c.CountyId);
        }
    }
    }
