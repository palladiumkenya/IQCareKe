using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class DeliveredBabyBirthInformationMapping : IEntityTypeConfiguration<DeliveredBabyBirthInformation>
    {
        public void Configure(EntityTypeBuilder<DeliveredBabyBirthInformation> builder)
        {
            builder.ToTable(nameof(DeliveredBabyBirthInformation))
                .Property(x => x.BreastFedWithinHour).HasColumnName("BreastFedWithinHr");

            builder.ToTable(nameof(DeliveredBabyBirthInformation))
           .Property(x => x.PatientDeliveryInformationId).HasColumnName("DeliveryId");

            builder.ToTable(nameof(DeliveredBabyBirthInformation))
           .Property(x => x.Comment).HasColumnName("BirthComments");
        }
    }
}
