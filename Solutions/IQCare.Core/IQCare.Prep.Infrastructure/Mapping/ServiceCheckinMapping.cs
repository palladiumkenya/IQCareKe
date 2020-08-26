using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class ServiceCheckinMapping : IEntityTypeConfiguration<ServiceCheckin>
    {
        public void Configure(EntityTypeBuilder<ServiceCheckin> builder)
        {
            builder.ToTable("ServiceCheckin")
                .HasKey(c => c.Id);
        }
    }
}
