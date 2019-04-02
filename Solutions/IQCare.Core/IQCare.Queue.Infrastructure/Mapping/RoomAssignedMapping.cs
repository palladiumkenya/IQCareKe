using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
    public class RoomAssignedMapping : IEntityTypeConfiguration<RoomStatus>
    {
        public void Configure(EntityTypeBuilder<RoomStatus> builder)
        {
            builder.ToTable(nameof(RoomStatus)).HasKey(x => x.Id);
        }
    }
}
    
