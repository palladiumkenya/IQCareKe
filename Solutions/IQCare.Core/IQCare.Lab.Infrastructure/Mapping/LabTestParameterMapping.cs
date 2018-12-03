using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabTestParameterMapping : IEntityTypeConfiguration<LabTestParameter>
    {
        public void Configure(EntityTypeBuilder<LabTestParameter> builder)
        {
            builder.ToTable("Mst_LabTestParameter").HasKey(c => c.Id);
        }
    }
}