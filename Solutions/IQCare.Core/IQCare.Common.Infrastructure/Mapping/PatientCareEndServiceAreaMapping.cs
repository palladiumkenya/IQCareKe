using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientCareEndServiceAreaMapping : IEntityTypeConfiguration<PatientCareEndingServiceArea>
    {
        public void Configure(EntityTypeBuilder<PatientCareEndingServiceArea> builder)
        {
            builder.ToTable("PatientCareEndServiceAreaView").HasKey(c => c.Id);
        }

    }
}