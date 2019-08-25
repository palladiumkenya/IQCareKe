using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public class PatientARVHistoryMapping : IEntityTypeConfiguration<PatientARVHistory>
    {
        public void Configure(EntityTypeBuilder<PatientARVHistory> builder)
        {
            builder.ToTable("PatientArvHistory").HasKey(c => c.Id);
        }
    }
}
