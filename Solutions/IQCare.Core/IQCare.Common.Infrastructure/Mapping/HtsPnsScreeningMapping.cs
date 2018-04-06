using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HtsPnsScreeningMapping : IEntityTypeConfiguration<HtsScreening>
    {
        public void Configure(EntityTypeBuilder<HtsScreening> builder)
        {
            builder.ToTable("HtsScreening")
                .HasKey(c => c.Id);
        }
    }
}