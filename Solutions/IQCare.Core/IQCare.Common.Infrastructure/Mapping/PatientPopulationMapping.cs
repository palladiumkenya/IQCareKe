using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientPopulationMapping : IEntityTypeConfiguration<PatientPopulation>
    {
        public void Configure(EntityTypeBuilder<PatientPopulation> builder)
        {
            builder.ToTable("PatientPopulation")
                .HasKey(c => c.Id);
        }
    }
}