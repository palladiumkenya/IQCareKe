using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class ParameterResultOptionMapping : IEntityTypeConfiguration<ParameterResultOption>
    {
        public void Configure(EntityTypeBuilder<ParameterResultOption> builder)
        {
            builder.ToTable("dtl_LabTestParameterResultOption").HasKey(c => c.Id);
        }
    }
}