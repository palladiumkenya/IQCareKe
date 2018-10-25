using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientDeliveryInfomationViewMapping : IEntityTypeConfiguration<PatientDeliveryInformationView>
    {
        public void Configure(EntityTypeBuilder<PatientDeliveryInformationView> builder)
        {
            builder.ToTable(nameof(PatientDeliveryInformationView));
        }
    }
}
