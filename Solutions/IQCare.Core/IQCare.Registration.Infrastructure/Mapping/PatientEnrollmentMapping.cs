using IQCare.Registration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Registration.Infrastructure.Mapping
{
    public class PatientEnrollmentMapping : IEntityTypeConfiguration<PatientEnrollment>
    {
        public void Configure(EntityTypeBuilder<PatientEnrollment> builder)
        {
            builder.ToTable("PatientEnrollment")
                .HasKey(e => e.Id);
        }
    }
}
