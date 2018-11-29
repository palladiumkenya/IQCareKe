
using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientCounsellingViewMapping : IEntityTypeConfiguration<PatientCounsellingView>
    {
        void IEntityTypeConfiguration<PatientCounsellingView>.Configure(EntityTypeBuilder<PatientCounsellingView> builder)
        {
            builder.ToTable("PatientCounsellingView")
                .HasKey(c => c.Id);
        }
    }
}