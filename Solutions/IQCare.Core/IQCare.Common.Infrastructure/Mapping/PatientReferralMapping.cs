using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientRefferalMapping : IEntityTypeConfiguration<PatientRefferal>
    {
        void IEntityTypeConfiguration<PatientRefferal>.Configure(EntityTypeBuilder<PatientRefferal> builder)
        {
            builder.ToTable("PatientReferral")
                .HasKey(c => c.Id);
        }
    }
}
