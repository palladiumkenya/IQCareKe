using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonEmergencyViewMapping : IEntityTypeConfiguration<PersonEmergencyView>
    {
        void IEntityTypeConfiguration<PersonEmergencyView>.Configure(EntityTypeBuilder<PersonEmergencyView> builder)
        {
            builder.ToTable("PersonEmergencyView")
                .HasKey(c => c.Id);
        }
    }
}