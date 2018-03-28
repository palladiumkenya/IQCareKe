using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HtsPnsScreeningMapping : IEntityTypeConfiguration<HtsPnsScreening>
    {
        public void Configure(EntityTypeBuilder<HtsPnsScreening> builder)
        {
            builder.ToTable("HtsPnsScreening")
                .HasKey(c => c.Id);
        }
    }
}