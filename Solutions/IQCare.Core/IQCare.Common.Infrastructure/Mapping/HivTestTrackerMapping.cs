using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class HivTestTrackerMapping : IEntityTypeConfiguration<HivTestTracker>
    {
        public void Configure(EntityTypeBuilder<HivTestTracker> builder)
        {
            builder.ToTable("HIVTestTracker").HasKey(c => c.Id);
        }
    }
}