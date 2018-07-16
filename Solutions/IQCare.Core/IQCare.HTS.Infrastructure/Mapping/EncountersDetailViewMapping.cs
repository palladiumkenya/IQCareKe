using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class EncountersDetailViewMapping : IEntityTypeConfiguration<EncountersDetailView>
    {
        public void Configure(EntityTypeBuilder<EncountersDetailView> builder)
        {
            builder.ToTable("HTS_EncountersDetailView")
                .HasKey(c => c.RowID);
        }
    }
}