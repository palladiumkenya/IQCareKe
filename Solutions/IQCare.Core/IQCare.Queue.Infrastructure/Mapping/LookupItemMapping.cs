using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
   public class LookupItemMapping:IEntityTypeConfiguration<LookupItem>
    {
        public void Configure(EntityTypeBuilder<LookupItem> builder)
        {
            builder.ToTable(nameof(LookupItem)).HasKey(x => x.Id);
        }
            
    }
}
