using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class HTSEncountersViewMapping : IEntityTypeConfiguration<HTSEncountersView>
    {
        public void Configure(EntityTypeBuilder<HTSEncountersView> builder)
        {
            builder.ToTable("HTS_EncountersView")
                .HasKey(c => c.RowID);
        }
    }
}