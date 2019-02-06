using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
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
