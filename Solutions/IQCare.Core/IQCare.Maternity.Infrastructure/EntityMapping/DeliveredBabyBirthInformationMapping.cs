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
            string tableName = nameof(DeliveredBabyBirthInformation);

            builder.ToTable(tableName).Property(x => x.BreastFedWithinHour).HasColumnName("BreastFedWithinHr");

            builder.ToTable(tableName).Property(x => x.PatientDeliveryInformationId).HasColumnName("DeliveryId");

            builder.ToTable(tableName).Property(x => x.Comment).HasColumnName("BirthComments");
            builder.ToTable(tableName).HasKey(x => x.Id);
        }
    }

    public class DeliveredBabyBirthInfoViewMapping : IEntityTypeConfiguration<DeliveredBabyBirthInfoView>
    {
        public void Configure(EntityTypeBuilder<DeliveredBabyBirthInfoView> builder)
        {
            builder.ToTable(nameof(DeliveredBabyBirthInfoView));
        }
    }
}
