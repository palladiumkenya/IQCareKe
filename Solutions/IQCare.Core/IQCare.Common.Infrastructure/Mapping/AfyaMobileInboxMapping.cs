using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AfyaMobileInboxMapping : IEntityTypeConfiguration<ApiInbox>
    {
        public void Configure(EntityTypeBuilder<ApiInbox> builder)
        {
            builder.ToTable("ApiInbox").HasKey(c => c.Id);
        }
    }
}