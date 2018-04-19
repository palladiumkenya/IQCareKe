using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientPopulationMapping : IEntityTypeConfiguration<PersonPopulation>
    {
        public void Configure(EntityTypeBuilder<PersonPopulation> builder)
        {
            builder.ToTable("PatientPopulation")
                .HasKey(c => c.Id);
        }
    }
}