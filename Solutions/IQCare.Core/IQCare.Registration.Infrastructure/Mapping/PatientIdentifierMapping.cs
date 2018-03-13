using IQCare.Registration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Registration.Infrastructure.Mapping
{
    public class PatientIdentifierMapping : IEntityTypeConfiguration<PatientIdentifier>
    {
        public void Configure(EntityTypeBuilder<PatientIdentifier> builder)
        {
            builder.ToTable("PatientIdentifier")
                .HasKey(e => e.Id);
        }
    }
}
