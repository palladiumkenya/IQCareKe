using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HeiFeedingMapping : IEntityTypeConfiguration<HeiFeeding>
    {
        void IEntityTypeConfiguration<HeiFeeding>.Configure(EntityTypeBuilder<HeiFeeding> builder)
        {
            builder.ToTable("HeiFeeding")
                .HasKey(c => c.Id);
        }
    }
}
