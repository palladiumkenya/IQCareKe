using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PregnancyLogMapping : IEntityTypeConfiguration<PregnancyLog>
    {
        public void Configure(EntityTypeBuilder<PregnancyLog> builder)
        {
            builder.ToTable("PregnancyLog").HasKey(x => x.Id);
        }
    }
}