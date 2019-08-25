using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientTransferInMapping : IEntityTypeConfiguration<PatientTransferIn>
    {
        public void Configure(EntityTypeBuilder<PatientTransferIn> builder)
        {
            builder.ToTable("PatientTransferIn")
                .HasKey(e => e.Id);
        }
    }
}
