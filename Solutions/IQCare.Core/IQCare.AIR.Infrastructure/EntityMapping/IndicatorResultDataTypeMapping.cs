using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class IndicatorResultDataTypeMapping : IEntityTypeConfiguration<IndicatorResultDataType>
    {
        public void Configure(EntityTypeBuilder<IndicatorResultDataType> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");
            builder.Property(x => x.Type).HasColumnName("Name");
            
            builder.ToTable("DataType");
        }
    }
}
