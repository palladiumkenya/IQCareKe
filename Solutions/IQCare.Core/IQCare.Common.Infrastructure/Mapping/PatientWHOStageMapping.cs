using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientWHOStageMapping : IEntityTypeConfiguration<PatientWHOStage>
    {
            public void Configure(EntityTypeBuilder<PatientWHOStage> builder)
            {
                builder.ToTable("PatientWHOStage").HasKey(c => c.Id);
            }

    }
}
