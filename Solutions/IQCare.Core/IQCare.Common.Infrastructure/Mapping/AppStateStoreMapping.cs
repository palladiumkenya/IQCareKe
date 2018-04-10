using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AppStateStoreMapping : IEntityTypeConfiguration<AppStateStore>
    {
        public void Configure(EntityTypeBuilder<AppStateStore> builder)
        {
            builder.ToTable("AppStateStore").HasKey(c => c.Id);
        }
    }
}