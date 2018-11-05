using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientPatnerTestingEntityMapping : IEntityTypeConfiguration<PatientPartnerTesting>
    {
        public void Configure(EntityTypeBuilder<PatientPartnerTesting> builder)
        {
            builder.ToTable(nameof(PatientPartnerTesting)).HasKey(x => x.Id);
        }
    }
}
