using IQCare.Registration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Registration.Infrastructure.Mapping
{
    public class PatientLinkageMapping : IEntityTypeConfiguration<PatientLinkage>
    {
        public void Configure(EntityTypeBuilder<PatientLinkage> builder)
        {
            builder.ToTable("PatientLinkage")
                .HasKey(e => e.Id);
        }
    }
}
