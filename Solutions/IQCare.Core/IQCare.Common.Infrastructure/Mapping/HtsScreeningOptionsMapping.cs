using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HtsScreeningOptionsMapping : IEntityTypeConfiguration<HtsScreeningOptions>
    {
        public void Configure(EntityTypeBuilder<HtsScreeningOptions> builder)
        {
            builder.ToTable("HtsScreeningOptions")
                .HasKey(c => c.Id);
        }
    }
}