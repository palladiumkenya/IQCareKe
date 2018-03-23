using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientConsentMapping : IEntityTypeConfiguration<PatientConsent>
    {
        public void Configure(EntityTypeBuilder<PatientConsent> builder)
        {
            builder.ToTable("PatientConsent")
                .HasKey(c => c.Id);
        }
    }
}