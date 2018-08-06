using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonEducationMapping : IEntityTypeConfiguration<PersonEducation>
    {
        void IEntityTypeConfiguration<PersonEducation>.Configure(EntityTypeBuilder<PersonEducation> builder)
        {
            builder.ToTable("PersonEducation")
                .HasKey(c => c.Id);
        }
    }
}
