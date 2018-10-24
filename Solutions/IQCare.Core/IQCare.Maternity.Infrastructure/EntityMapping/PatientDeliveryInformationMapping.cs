using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientDeliveryInformationMapping : IEntityTypeConfiguration<PatientDeliveryInformation>
    {
        public void Configure(EntityTypeBuilder<PatientDeliveryInformation> builder)
        {
            builder.ToTable("PatientDelivery").Property(x => x.Id).HasColumnName("DeliveryID");
            builder.ToTable("PatientDelivery").Property(x => x.ProfileId).HasColumnName("ProfileID");
            builder.ToTable("PatientDelivery").Property(x => x.PatientMasterVisitId).HasColumnName("PatientMasterVisitID");

        }
    }
}
