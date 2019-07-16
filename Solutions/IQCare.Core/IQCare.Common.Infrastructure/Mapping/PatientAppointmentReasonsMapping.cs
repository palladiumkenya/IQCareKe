using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientAppointmentReasonsMapping : IEntityTypeConfiguration<PatientAppointmentReasons>
    {
        public void Configure(EntityTypeBuilder<PatientAppointmentReasons> builder)
        {
            builder.ToTable("PatientAppointmentReasons").HasKey(c => c.Id);
        }
    }
}