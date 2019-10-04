using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
   public class LookupMasterMapping:IEntityTypeConfiguration<LookupMaster>
    {
        public void Configure(EntityTypeBuilder<LookupMaster> builder)
        {
            builder.ToTable(nameof(LookupMaster)).HasKey(x => x.Id);
        }
           
    }
}
