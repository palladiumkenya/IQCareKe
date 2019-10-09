using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.Infrastructure.Mapping
{
    public class LookupItemViewMapping:IEntityTypeConfiguration<LookupItemView>
    {
        public void Configure(EntityTypeBuilder<LookupItemView> builder)
        {
            builder.ToTable(nameof(LookupItemView)).HasKey(c => c.RowID); 
        }

        
    }
}
