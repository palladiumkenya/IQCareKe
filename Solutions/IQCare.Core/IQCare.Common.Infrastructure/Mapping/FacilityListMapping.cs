using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class FacilityListMapping : IEntityTypeConfiguration<FacilityList>
    {
        public void Configure(EntityTypeBuilder<FacilityList> builder)
        {
            builder.ToTable("FacilityList").HasKey(c => c.id);
        }
    }
}