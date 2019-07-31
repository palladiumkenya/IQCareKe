using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
   public  class PatientAppointmentsServiceAreaMapping : IEntityTypeConfiguration<Api_AppointmentsServiceAreaView>
    {
        void IEntityTypeConfiguration<Api_AppointmentsServiceAreaView>.Configure(EntityTypeBuilder<Api_AppointmentsServiceAreaView> builder)
        {
            builder.ToTable("Api_AppointmentsServiceAreaView")
                .HasKey(c => c.Id);
        }
    }
}
