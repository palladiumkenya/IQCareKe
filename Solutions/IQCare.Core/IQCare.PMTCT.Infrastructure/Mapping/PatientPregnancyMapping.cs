using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientPregnancyMapping :IEntityTypeConfiguration<PatientPregnancy>
    {
        public void Configure(EntityTypeBuilder<PatientPregnancy> builder)
        {
            builder.ToTable("Pregnancy")
                .HasKey(c => c.Id);
        }
    }
}