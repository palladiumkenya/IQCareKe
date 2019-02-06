using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientAppointmentViewMaping : IEntityTypeConfiguration<Api_PatientAppointmentsView>
    {

        void IEntityTypeConfiguration<Api_PatientAppointmentsView>.Configure(EntityTypeBuilder<Api_PatientAppointmentsView> builder)
        {
            builder.ToTable("Api_PatientAppointmentsView")
                .HasKey(c => c.Id);
        }
        //public void Configure(EntityTypeBuilder<Api_PatientAppointmentsView> builder)
        //{
        //    builder.ToTable("Api_PatientAppointmentsView").HasKey(x => x.Id);
        //}
    }
}