using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientDischargeInformationViewMapping : IEntityTypeConfiguration<PatientDischargeInformationView>
    {
        public void Configure(EntityTypeBuilder<PatientDischargeInformationView> builder)
        {
            builder.ToTable(nameof(PatientDischargeInformationView)).HasKey(x => x.Id);
        }
    }
}
