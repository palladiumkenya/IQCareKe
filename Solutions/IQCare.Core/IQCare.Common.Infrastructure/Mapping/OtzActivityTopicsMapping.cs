using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class OtzActivityTopicsMapping : IEntityTypeConfiguration<OtzActivityTopics>
    {
        public void Configure(EntityTypeBuilder<OtzActivityTopics> builder)
        {
            builder.ToTable("OtzActivityTopics").HasKey(c => c.Id);
        }
    }
}
