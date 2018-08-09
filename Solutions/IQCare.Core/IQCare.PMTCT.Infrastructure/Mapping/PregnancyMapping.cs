using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PregnancyMapping :IEntityTypeConfiguration<Pregnancy>
    {
        public void Configure(EntityTypeBuilder<Pregnancy> builder)
        {
            builder.ToTable("Pregnancy")
                .HasKey(c => c.Id);
        }
    }
}