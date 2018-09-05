using System;
using System.Collections.Generic;
using System.Text;
using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientPartnerTestingMapping: IEntityTypeConfiguration<PatientPartnerTesting>
    {

        void IEntityTypeConfiguration<PatientPartnerTesting>.Configure(EntityTypeBuilder<PatientPartnerTesting> builder)
        {
            builder.ToTable("PatientPartnerTesting")
                .HasKey(c => c.Id);
        }
    }
}
