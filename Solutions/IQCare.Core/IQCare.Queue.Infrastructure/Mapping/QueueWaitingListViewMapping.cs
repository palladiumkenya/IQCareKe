using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;
namespace IQCare.Queue.Infrastructure.Mapping
{
   public class QueueWaitingListViewMapping : IEntityTypeConfiguration<WaitingListView>
    {
        public void Configure(EntityTypeBuilder<WaitingListView> builder)
        {
            builder.ToTable(nameof(WaitingListView)).HasKey(x => x.Id);
        }
    }
    
}
