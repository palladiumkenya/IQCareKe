using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class PharmacyDrugVisitDetailsMapping : IEntityTypeConfiguration<PharmacyDrugVisitDetails>
    {
        public void Configure(EntityTypeBuilder<PharmacyDrugVisitDetails> builder)
        {
            builder.ToTable("PharmacyDrugVisitDetailsView").HasKey(c => c.RowID);
        }
    }
}
