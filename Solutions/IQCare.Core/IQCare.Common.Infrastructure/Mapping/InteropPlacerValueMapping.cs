using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class InteropPlacerValueMapping : IEntityTypeConfiguration<InteropPlacerValue>
    {
        public void Configure(EntityTypeBuilder<InteropPlacerValue> builder)
        {
            builder.ToTable("Interop_PlacerValues").HasKey(c => c.Id);
        }
    }
}