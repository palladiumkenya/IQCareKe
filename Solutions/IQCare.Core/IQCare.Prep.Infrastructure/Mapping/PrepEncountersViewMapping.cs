using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PrepEncountersViewMapping : IEntityTypeConfiguration<PrepEncountersView>
    {
        public void Configure(EntityTypeBuilder<PrepEncountersView> builder)
        {
            builder.ToTable("PREP_EncountersView")
                .HasKey(c => c.RowID);
        }
    }
}