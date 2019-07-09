using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientAllergyViewMapping : IEntityTypeConfiguration<PatientAllergyView>
    {
        public void Configure(EntityTypeBuilder<PatientAllergyView> builder)
        {
            builder.ToTable("PatientAllergyView")
                .HasKey(c => c.Id);
        }
    }
}