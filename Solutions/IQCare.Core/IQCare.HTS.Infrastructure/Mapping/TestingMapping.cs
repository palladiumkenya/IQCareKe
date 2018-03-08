using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class TestingMapping : IEntityTypeConfiguration<Testing>
    {
        public void Configure(EntityTypeBuilder<Testing> builder)
        {
            builder.ToTable("Testing")
                .HasKey(c => c.Id);
        }
    }
}
