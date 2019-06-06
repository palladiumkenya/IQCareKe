using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientAdverseEventMapping : IEntityTypeConfiguration<PatientAdverseEvent>
    {
        public void Configure(EntityTypeBuilder<PatientAdverseEvent> builder)
        {
            builder.ToTable("AdverseEvent").HasKey(c => c.Id);
        }
    }
}