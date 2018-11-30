using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabOrderTestMapping : IEntityTypeConfiguration<LabOrderTest>
    {
        public void Configure(EntityTypeBuilder<LabOrderTest> builder)
        {
            builder.ToTable("dtl_LabOrderTest").HasKey(c => c.Id);
        }
    }
}