using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class HeiPatientIcfActionMapping : IEntityTypeConfiguration<HEiPatientIcfAction>
    {

        void IEntityTypeConfiguration<HEiPatientIcfAction>.Configure(EntityTypeBuilder<HEiPatientIcfAction> builder)
        {
            builder.ToTable("PatientIcfAction")
                .HasKey(c => c.Id);
        }
    }
}