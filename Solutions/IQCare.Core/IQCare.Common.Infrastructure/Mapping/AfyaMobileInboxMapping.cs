using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class AfyaMobileInboxMapping : IEntityTypeConfiguration<AfyaMobileInbox>
    {
        public void Configure(EntityTypeBuilder<AfyaMobileInbox> builder)
        {
            builder.ToTable("AfyaMobileInbox").HasKey(c => c.Id);
        }
    }
}