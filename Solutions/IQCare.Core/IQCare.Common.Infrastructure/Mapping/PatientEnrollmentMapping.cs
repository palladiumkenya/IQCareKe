using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientEnrollmentMapping : IEntityTypeConfiguration<PatientEnrollment>
    {
        public void Configure(EntityTypeBuilder<PatientEnrollment> builder)
        {
            builder.ToTable("PatientEnrollment")
                .HasKey(e => e.Id);
        }
    }
}