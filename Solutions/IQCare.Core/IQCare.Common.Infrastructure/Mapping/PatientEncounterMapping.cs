using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientEncounterMapping : IEntityTypeConfiguration<PatientEncounter>
    {
        public void Configure(EntityTypeBuilder<PatientEncounter> builder)
        {
            builder.ToTable("PatientEncounter")
                .HasKey(c => c.Id);
        }
    }
}