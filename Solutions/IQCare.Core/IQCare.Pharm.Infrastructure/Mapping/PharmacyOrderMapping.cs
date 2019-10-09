using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IQCare.Pharm.Core.Models;


namespace IQCare.Pharm.Infrastructure.Mapping
{
    public class PharmacyOrderMapping : IEntityTypeConfiguration<PatientPharmacyOrder>
    {
        public void Configure(EntityTypeBuilder<PatientPharmacyOrder> builder)
        {
            builder.ToTable("ord_PatientPharmacyOrder").HasKey(c => c.ptn_pharmacy_pk);
        }
    }
}
