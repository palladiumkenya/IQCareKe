using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.Infrastructure.Mapping
{
   public class LookupMasterItemMapping:IEntityTypeConfiguration<LookupMasterItem>
    { 
        public void Configure(EntityTypeBuilder<LookupMasterItem> builder)
        {
            builder.ToTable(nameof(LookupMasterItem)).HasKey(c => c.RowID); ;
        }
    }
}
