using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HeiProfileMapping : IEntityTypeConfiguration<HeiProfile>
    {
        void IEntityTypeConfiguration<HeiProfile>.Configure(EntityTypeBuilder<HeiProfile> builder)
        {
            builder.ToTable("HEIProfile")
                .HasKey(c => c.Id);
        }

    }
}
