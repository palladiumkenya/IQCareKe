using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PartnersViewMapping : IEntityTypeConfiguration<PartnersView>
    {
        public void Configure(EntityTypeBuilder<PartnersView> builder)
        {
            builder.ToTable("HTS_PartnersView")
                .HasKey(c => c.RowID);
        }
    }
}