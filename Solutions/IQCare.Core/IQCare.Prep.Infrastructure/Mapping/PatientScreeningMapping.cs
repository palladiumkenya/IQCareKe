using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Prep.Infrastructure.Mapping
{
   public  class PatientScreeningMapping : IEntityTypeConfiguration<PatientScreening>
    {
        public void Configure(EntityTypeBuilder<PatientScreening> builder)
        {
            builder.ToTable("PatientScreening")
                .HasKey(c => c.Id);
        }
    }
}
