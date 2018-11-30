using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PhysicalExaminationMapping : IEntityTypeConfiguration<PhysicalExamination>
    {
        public void Configure(EntityTypeBuilder<PhysicalExamination> builder)
        {
            builder.ToTable("PhysicalExamination")
                .HasKey(c => c.Id);
        }
    }
}
