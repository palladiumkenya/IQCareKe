using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HEIMilestoneMapping : IEntityTypeConfiguration<HEIMilestone>
    {
        public void Configure(EntityTypeBuilder<HEIMilestone> builder)
        {
            builder.ToTable("PatientMilestone")
                .HasKey(c => c.Id);
        }
    }
}
