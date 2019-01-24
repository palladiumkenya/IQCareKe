using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class HEIEncounterMapping : IEntityTypeConfiguration<HEIEncounter>
    {
        public void Configure(EntityTypeBuilder<HEIEncounter> builder)
        {
            builder.ToTable("HEIEncounter").HasKey(c => c.Id);
        }
    }
}