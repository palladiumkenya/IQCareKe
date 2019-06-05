using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientMasterVisitMapping : IEntityTypeConfiguration<PatientMasterVisit>
    {
        public void Configure(EntityTypeBuilder<PatientMasterVisit> builder)
        {
            builder.ToTable("PatientMasterVisit")
                .HasKey(c => c.Id);
        }
    }
}
