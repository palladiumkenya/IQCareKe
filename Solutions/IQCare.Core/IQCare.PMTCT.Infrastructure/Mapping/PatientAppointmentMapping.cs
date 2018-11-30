
using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientAppointmentMapping : IEntityTypeConfiguration<PatientAppointment>
    {
        void IEntityTypeConfiguration<PatientAppointment>.Configure(EntityTypeBuilder<PatientAppointment> builder)
        {
            builder.ToTable("PatientAppointment")
                .HasKey(c => c.Id);
        }
    }
}