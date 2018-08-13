using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    class PersonOccupationMapping : IEntityTypeConfiguration<PersonOccupation>
    {
        public void Configure(EntityTypeBuilder<PersonOccupation> builder)
        {
            builder.ToTable("PersonOccupation")
                .HasKey(c => c.Id);
        }
    }
    
    
}
