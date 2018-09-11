using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class LookupItemMapping : IEntityTypeConfiguration<LookupItem>
    {

        public void Configure(EntityTypeBuilder<LookupItem> builder)
        {
            builder.ToTable("LookupItem").HasKey(c => c.Id);
        }
    }
}
