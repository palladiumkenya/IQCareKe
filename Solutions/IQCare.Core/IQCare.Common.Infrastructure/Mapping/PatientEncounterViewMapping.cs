using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientEncounterViewMapping : IEntityTypeConfiguration<PatientEncounterView>
    {
        public void Configure(EntityTypeBuilder<PatientEncounterView> builder)
        {
            builder.ToTable("PatientEncounterView").HasKey(c => c.Id);
        }
    }
}