using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class FrequencyMapping  : IEntityTypeConfiguration<Frequency>
    {
        public void Configure(EntityTypeBuilder<Frequency> builder)
        {
            builder.ToTable("mst_Frequency").HasKey(c => c.ID);
        }
    }
}