using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    class HEIMilestoneMapping : IEntityTypeConfiguration<HEIMilestone>
    {
        void IEntityTypeConfiguration<HEIMilestone>.Configure(EntityTypeBuilder<HEIMilestone> builder)
        {
            builder.ToTable("HeiMilestone")
                .HasKey(c => c.Id);
        }
    }
}
