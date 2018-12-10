using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabTestMapping : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.ToTable("mst_LabTestMaster").HasKey(c => c.Id);
        }
    }
}