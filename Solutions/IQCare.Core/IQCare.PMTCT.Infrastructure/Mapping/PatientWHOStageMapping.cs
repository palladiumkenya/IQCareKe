using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientWhoStageMapping : IEntityTypeConfiguration<PatientWHOStage>
    {
        public void Configure(EntityTypeBuilder<PatientWHOStage> builder)
        {
            builder.ToTable("PatientWHOStage")
                .HasKey(c => c.Id);
        }
    }
}
