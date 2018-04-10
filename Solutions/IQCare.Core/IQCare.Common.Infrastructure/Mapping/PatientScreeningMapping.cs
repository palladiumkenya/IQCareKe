using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientScreeningMapping : IEntityTypeConfiguration<PatientScreening>
    {
        public void Configure(EntityTypeBuilder<PatientScreening> builder)
        {
            builder.ToTable("PatientScreening")
                .HasKey(e => e.Id);
        }
    }
}