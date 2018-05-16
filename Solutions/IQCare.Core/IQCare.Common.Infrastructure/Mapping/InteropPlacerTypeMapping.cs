using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class InteropPlacerTypeMapping : IEntityTypeConfiguration<InteropPlacerType>
    {
        public void Configure(EntityTypeBuilder<InteropPlacerType> builder)
        {
            builder.ToTable("Interop_PlacerType").HasKey(c => c.Id);
        }
    }
}