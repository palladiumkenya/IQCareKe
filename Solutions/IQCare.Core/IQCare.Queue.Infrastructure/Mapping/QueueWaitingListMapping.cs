using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
   public  class QueueWaitingListMapping:IEntityTypeConfiguration<QueueWaitingList>
    { 
        public void Configure(EntityTypeBuilder<QueueWaitingList> builder)
        {
            builder.ToTable(nameof(QueueWaitingList)).HasKey(x => x.Id);
        }
    }
}
