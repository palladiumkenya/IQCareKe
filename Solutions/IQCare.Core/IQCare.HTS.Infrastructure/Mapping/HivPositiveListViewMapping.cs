using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class HivPositiveListViewMapping : IEntityTypeConfiguration<HivPositiveListView>
    {
        public void Configure(EntityTypeBuilder<HivPositiveListView> builder)
        {
            builder.ToTable("HIVPositiveListView").HasKey(c => c.RowId);
        }
    }
}