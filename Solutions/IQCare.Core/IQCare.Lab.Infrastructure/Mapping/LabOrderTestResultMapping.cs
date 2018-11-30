using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabOrderTestResultMapping : IEntityTypeConfiguration<LabOrderTestResult>
    {
        public void Configure(EntityTypeBuilder<LabOrderTestResult> builder)
        {
            builder.ToTable("dtl_LabOrderTestResult").HasKey(c => c.Id);
        }
    }
}