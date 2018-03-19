using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientIdentifierMapping : IEntityTypeConfiguration<PatientIdentifier>
    {
        public void Configure(EntityTypeBuilder<PatientIdentifier> builder)
        {
            builder.ToTable("PatientIdentifier")
                .HasKey(e => e.Id);
        }
    }
}