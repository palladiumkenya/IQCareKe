using IQCare.PMTCT.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class VisitDetailsMapping : IEntityTypeConfiguration<VisitDetails>
    {
        public void Configure(EntityTypeBuilder<VisitDetails> builder)
        {
            builder.ToTable("VisitDetails")
                .HasKey(c => c.Id);
        }
    }
}