using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Queue.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Queue.Infrastructure.Mapping
{
    public class ServiceAreaMapping:IEntityTypeConfiguration<ServiceArea>
    {
        public void Configure(EntityTypeBuilder<ServiceArea> builder)
        {
            builder.ToTable(nameof(ServiceArea)).HasKey(x => x.Id);
        }
    }
}
