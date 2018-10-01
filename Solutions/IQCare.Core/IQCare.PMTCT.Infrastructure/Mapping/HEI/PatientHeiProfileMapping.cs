using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class PatientHeiProfileMapping : IEntityTypeConfiguration<PatientHeiProfile>
    {
        void IEntityTypeConfiguration<PatientHeiProfile>.Configure(EntityTypeBuilder<PatientHeiProfile> builder)
        {
            builder.ToTable("HEIProfile")
                .HasKey(c => c.Id);
        }

    }
}
