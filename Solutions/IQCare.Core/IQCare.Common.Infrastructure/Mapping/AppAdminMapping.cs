using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AppAdminMapping : IEntityTypeConfiguration<AppAdmin>
    {
        public void Configure(EntityTypeBuilder<AppAdmin> builder)
        {
            builder.ToTable("AppAdmin").HasKey(c => c.Id);
        }
    }
}