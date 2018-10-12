using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HeiIptMapping: IEntityTypeConfiguration<HeiIpt>
    {

        void IEntityTypeConfiguration<HeiIpt>.Configure(EntityTypeBuilder<HeiIpt> builder)
        {
            builder.ToTable("PatientIpt")
                .HasKey(c => c.Id);
        }
    }
}