using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientWhoStageMapping : IEntityTypeConfiguration<PatientWhoStage>
    {
        public void Configure(EntityTypeBuilder<PatientWhoStage> builder)
        {
            builder.ToTable("PatientWHOStage")
                .HasKey(c => c.Id);
        }
    }
}
