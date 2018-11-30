using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonTreatmentSupporterMapping : IEntityTypeConfiguration<PersonTreatmentSupporter>
    {
        public void Configure(EntityTypeBuilder<PersonTreatmentSupporter> builder)
        {
            builder.ToTable("PatientTreatmentSupporter").HasKey(c => c.Id);
        }
    }
}