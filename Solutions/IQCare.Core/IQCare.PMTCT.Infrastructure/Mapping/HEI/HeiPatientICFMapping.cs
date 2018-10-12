using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HeiPatientIcfMapping : IEntityTypeConfiguration<HeiPatientIcf>
    {
        void IEntityTypeConfiguration<HeiPatientIcf>.Configure(EntityTypeBuilder<HeiPatientIcf> builder)
        {
            builder.ToTable("PatientIcf")
                .HasKey(c => c.Id);
        }
    }
}