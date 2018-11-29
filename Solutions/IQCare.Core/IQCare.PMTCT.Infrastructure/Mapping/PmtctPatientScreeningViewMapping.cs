using IQCare.PMTCT.Core.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PmtctPatientScreeningViewMapping : IEntityTypeConfiguration<PmtctPatientScreeningView>
    {
        void IEntityTypeConfiguration<PmtctPatientScreeningView>.Configure(EntityTypeBuilder<PmtctPatientScreeningView> builder)
        {
            builder.ToTable("pmtctPatientScreeningView")
                .HasKey(c => c.Id);
        }
    }
}