using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
  public  class PatientReferralMapping : IEntityTypeConfiguration<PatientReferral>
    {
        public void Configure(EntityTypeBuilder<PatientReferral> builder)
        {
            builder.ToTable("PatientReferral")
                .HasKey(c => c.Id);
        }
    }
}
