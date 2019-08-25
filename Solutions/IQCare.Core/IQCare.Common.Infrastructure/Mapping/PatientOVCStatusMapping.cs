using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientOVCStatusMapping : IEntityTypeConfiguration<PatientOVCStatus>
    {
        public void Configure(EntityTypeBuilder<PatientOVCStatus> builder)
        {
            builder.ToTable("PatientOVCStatus").HasKey(c => c.Id);
        }
    }
}
