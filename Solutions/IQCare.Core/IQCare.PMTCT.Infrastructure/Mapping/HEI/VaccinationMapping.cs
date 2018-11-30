using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class VaccinationMapping : IEntityTypeConfiguration<Vaccination>
    {
        void IEntityTypeConfiguration<Vaccination>.Configure(EntityTypeBuilder<Vaccination> builder)
        {
            builder.ToTable("Vaccination")
                .HasKey(c => c.Id);
        }
    }
}
