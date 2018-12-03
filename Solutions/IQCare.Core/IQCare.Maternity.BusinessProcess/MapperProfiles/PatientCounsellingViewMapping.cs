using IQCare.Maternity.BusinessProcess.Queries.ANC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
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