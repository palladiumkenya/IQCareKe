using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Mapping
{
   public class PatientClinicalNotesMapping : IEntityTypeConfiguration<PatientClinicalNotes>
    {
        public void Configure(EntityTypeBuilder<PatientClinicalNotes> builder)
        {
            builder.ToTable("PatientClinicalNotes")
                .HasKey(c => c.Id);
        }
    }
}
