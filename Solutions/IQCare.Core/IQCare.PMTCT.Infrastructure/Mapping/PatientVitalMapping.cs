using System;
using System.Collections.Generic;
using System.Text;
using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientVitalMapping : IEntityTypeConfiguration<PatientVital>
    {
        public void Configure(EntityTypeBuilder<PatientVital> builder)
        {
            builder.ToTable("PatientVitals").Property(x => x.DateCreated).HasColumnName("CreateDate");
        }
    }
}
