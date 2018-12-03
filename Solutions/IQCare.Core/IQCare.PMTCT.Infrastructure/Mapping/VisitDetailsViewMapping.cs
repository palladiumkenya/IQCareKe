using IQCare.PMTCT.Core.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class VisitDetailsViewMapping : IEntityTypeConfiguration<VisitDetailsView>
    {
        public void Configure(EntityTypeBuilder<VisitDetailsView> builder)
        {
            builder.ToTable("VisitDetailsView")
                .HasKey(c => c.Id);
        }
    }
}