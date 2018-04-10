using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class TracingMapping : IEntityTypeConfiguration<Tracing>
    {
        public void Configure(EntityTypeBuilder<Tracing> builder)
        {
            builder.ToTable("Tracing")
                .HasKey(c => c.Id);
        }
    }
}