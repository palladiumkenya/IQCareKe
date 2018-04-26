using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class LookupItemViewMapping : IEntityTypeConfiguration<LookupItemView>
    {
        public void Configure(EntityTypeBuilder<LookupItemView> builder)
        {
            builder.ToTable("LookupItemView")
                .HasKey(c => c.RowID);
        }
    }
}