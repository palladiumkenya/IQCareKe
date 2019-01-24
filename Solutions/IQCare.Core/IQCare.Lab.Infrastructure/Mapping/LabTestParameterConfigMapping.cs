using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabTestParameterConfigMapping : IEntityTypeConfiguration<LabTestParameterConfig>
    {
        public void Configure(EntityTypeBuilder<LabTestParameterConfig> builder)
        {
            builder.ToTable("dtl_LabTestParameterConfig").HasKey(c => c.Id);
        }
    }
}