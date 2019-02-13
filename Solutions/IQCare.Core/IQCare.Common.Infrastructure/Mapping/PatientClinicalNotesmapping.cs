using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class PatientClinicalNotesmapping : IEntityTypeConfiguration<PatientClinicalNotes>
    {
        public void Configure(EntityTypeBuilder<PatientClinicalNotes> builder)
        {
            builder.ToTable("PatientClinicalNotes").HasKey(c => c.Id);
        }
    }
}
