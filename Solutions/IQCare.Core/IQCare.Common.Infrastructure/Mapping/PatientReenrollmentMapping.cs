using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public  class PatientReenrollmentMapping :IEntityTypeConfiguration<PatientReenrollment>
    {
        public void Configure(EntityTypeBuilder<PatientReenrollment> builder)
        {
            builder.ToTable("PatientReenrollment");
        }
    }
}
