using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public  class PatientCareEndingMapping :IEntityTypeConfiguration<PatientCareEnding>
    {
        public void Configure(EntityTypeBuilder<PatientCareEnding> builder)
        {
            builder.ToTable("PatientCareending").HasKey(c => c.Id);
        }

    }
}
