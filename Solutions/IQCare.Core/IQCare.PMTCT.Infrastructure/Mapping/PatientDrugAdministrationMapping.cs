using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
   public class PatientDrugAdministrationMapping : IEntityTypeConfiguration<PatientProfile>
    {
        public void Configure(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.ToTable("PatientDrugAdministration")
                .HasKey(c => c.Id);
        }
    }
}
