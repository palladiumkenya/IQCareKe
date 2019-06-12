using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PregnancyIndicatorMapping : IEntityTypeConfiguration<PregnancyIndicator>
    {
        public void Configure(EntityTypeBuilder<PregnancyIndicator> builder)
        {
            builder.ToTable("PregnancyIndicator").HasKey(x => x.Id);
        }
    }
}