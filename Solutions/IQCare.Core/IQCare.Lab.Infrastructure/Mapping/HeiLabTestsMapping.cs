using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class HeiLabTestsMapping : IEntityTypeConfiguration<HeiLabTests>
    {
        public void Configure(EntityTypeBuilder<HeiLabTests> builder)
        {
            builder.ToTable("HeiLabTests").HasKey(c => c.Id);
        }
    }
}