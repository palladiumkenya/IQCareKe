using IQCare.PMTCT.Core.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PmtctReferralViewMapping : IEntityTypeConfiguration<PmtctReferralView>
    {
        void IEntityTypeConfiguration<PmtctReferralView>.Configure(EntityTypeBuilder<PmtctReferralView> builder)
        {
            builder.ToTable("PmtctReferralView")
                .HasKey(c => c.Id);
        }
    }
}