using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonVitalsViewMapping : IEntityTypeConfiguration<PersonVitalsView>
    {
        public void Configure(EntityTypeBuilder<PersonVitalsView> builder)
        {
            builder.ToTable("PersonVitalsView")
                .HasKey(c => c.Id);
        }
    }
}
