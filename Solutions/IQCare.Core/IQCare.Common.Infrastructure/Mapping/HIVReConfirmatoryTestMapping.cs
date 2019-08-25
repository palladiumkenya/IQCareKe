using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HIVReConfirmatoryTestMapping : IEntityTypeConfiguration<HIVReConfirmatoryTest>
    {
        public void Configure(EntityTypeBuilder<HIVReConfirmatoryTest> builder)
        {
            builder.ToTable("HIVReConfirmatoryTest").HasKey(c => c.Id);
        }
    }
}