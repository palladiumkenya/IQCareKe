using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientCircumcisionStatusMapping : IEntityTypeConfiguration<PatientCircumcisionStatus>
    {
        public void Configure(EntityTypeBuilder<PatientCircumcisionStatus> builder)
        {
            builder.ToTable("PatientCircumcisionStatus")
                .HasKey(c => c.Id);
        }
    }
}