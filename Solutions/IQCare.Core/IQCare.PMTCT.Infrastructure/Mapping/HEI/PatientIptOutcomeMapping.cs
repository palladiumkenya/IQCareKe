using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class PatientIptOutcomeMapping : IEntityTypeConfiguration<PatientIptOutcome>
    {
        void IEntityTypeConfiguration<PatientIptOutcome>.Configure(EntityTypeBuilder<PatientIptOutcome> builder)
        {
            builder.ToTable("PatientIptOutcome")
                .HasKey(c => c.Id);
        }
    }
}