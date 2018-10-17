using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class PersonOccupationMapping : IEntityTypeConfiguration<PersonOccupation>
    {
        void IEntityTypeConfiguration<PersonOccupation>.Configure(EntityTypeBuilder<PersonOccupation> builder)
        {
            builder.ToTable("PersonOccupation")
                .HasKey(c => c.Id);
        }
    }
    
    
}
