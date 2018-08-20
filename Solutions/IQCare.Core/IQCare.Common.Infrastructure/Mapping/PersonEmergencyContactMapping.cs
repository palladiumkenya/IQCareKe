using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
   public class PersonEmergencyContactMapping : IEntityTypeConfiguration<PersonEmergencyContact>
    {
        void IEntityTypeConfiguration<PersonEmergencyContact>.Configure(EntityTypeBuilder<PersonEmergencyContact> builder)
        {
            builder.ToTable("PersonEmergencyContact")
                .HasKey(c => c.Id);
        }
    }
}
