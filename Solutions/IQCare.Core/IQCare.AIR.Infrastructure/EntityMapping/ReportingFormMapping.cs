using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.AIR.Infrastructure.EntityMapping
{
    public class ReportingFormMapping : IEntityTypeConfiguration<ReportingForm>
    {
        public void Configure(EntityTypeBuilder<ReportingForm> builder)
        {
            builder.Property(x => x.DateCreated).HasColumnName("CreateDate");    
            
            builder.ToTable("Form");
        }
    }
}
