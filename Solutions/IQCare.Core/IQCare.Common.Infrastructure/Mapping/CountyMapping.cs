using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class CountyMapping : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.ToTable("County").HasKey(c => c.CountyId);
        }
    }
}