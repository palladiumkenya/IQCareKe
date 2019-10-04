using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Queue.Core.Models;

namespace IQCare.Queue.Infrastructure.Mapping
{
   public class ServiceRoomViewMapping:IEntityTypeConfiguration<ServiceRoomView>
    {
        public void Configure(EntityTypeBuilder<ServiceRoomView> builder)
        {
            builder.ToTable(nameof(ServiceRoomView)).HasKey(x => x.Id);
        }
    }
}
