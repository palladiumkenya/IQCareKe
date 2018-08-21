using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
   public class PatientDrugAdministrationMapping : IEntityTypeConfiguration<PatientDrugAdministration>
    {
        public void Configure(EntityTypeBuilder<PatientDrugAdministration> builder)
        {
            builder.ToTable("PatientDrugAdministration")
                .HasKey(c => c.Id);
        }
    }
}
