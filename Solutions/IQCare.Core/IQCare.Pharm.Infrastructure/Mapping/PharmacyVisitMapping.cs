using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;
namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class PharmacyVisitMapping : IEntityTypeConfiguration<PharmacyVisit>
    {
        public void Configure(EntityTypeBuilder<PharmacyVisit> builder)
        {
            builder.ToTable("PharmacyVisitDetailsView").HasKey(c => c.VisitID);
        }
    }
}