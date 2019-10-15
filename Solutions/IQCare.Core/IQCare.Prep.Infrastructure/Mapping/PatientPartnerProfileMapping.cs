using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientPartnerProfileMapping : IEntityTypeConfiguration<PatientPartnerProfile>
    {
        public void Configure(EntityTypeBuilder<PatientPartnerProfile> builder)
        {
            builder.ToTable("PatientPartnerProfile")
                .HasKey(c => c.Id);
        }
    }
}
