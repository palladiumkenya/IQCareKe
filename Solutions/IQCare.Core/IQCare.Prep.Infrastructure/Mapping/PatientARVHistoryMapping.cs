using IQCare.Prep.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientARVHistoryMapping : IEntityTypeConfiguration<PatientARVHistory>
    {
        public void Configure(EntityTypeBuilder<PatientARVHistory> builder)
        {
            builder.ToTable("PatientArvHistory")
                .HasKey(c => c.Id);
        }
    }
}
