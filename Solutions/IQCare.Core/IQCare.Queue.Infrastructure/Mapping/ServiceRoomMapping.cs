using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Queue.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Queue.Infrastructure.Mapping
{
    public class ServiceRoomMapping : IEntityTypeConfiguration<ServiceRoom>
    {

        public void Configure(EntityTypeBuilder<ServiceRoom> builder)

        {
            builder.ToTable(nameof(ServiceRoom)).HasKey(x => x.Id);
        }
    }
}
