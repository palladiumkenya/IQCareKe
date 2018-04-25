using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AppStateStoreObjectsMapping : IEntityTypeConfiguration<AppStateStoreObjects>
    {
        public void Configure(EntityTypeBuilder<AppStateStoreObjects> builder)
        {
            builder.ToTable("AppStateStoreObjects").HasKey(c => c.Id);
        }
    }
}