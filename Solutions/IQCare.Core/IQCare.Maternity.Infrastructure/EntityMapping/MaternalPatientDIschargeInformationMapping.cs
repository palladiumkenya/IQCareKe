using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class MaternalPatientDischargeInformationMapping : IEntityTypeConfiguration<MaternalPatientDischargeInformation>
    {
        public void Configure(EntityTypeBuilder<MaternalPatientDischargeInformation> builder)
        {
            builder.ToTable("PatientOutcome");
        }
    }
}
