using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace IQCare.Common.Infrastructure.Mapping
{
   public  class OvcEnrollmentFormMapping : IEntityTypeConfiguration<OvcEnrollmentForm>
    {
        public void Configure(EntityTypeBuilder<OvcEnrollmentForm> builder)
        {
            builder.ToTable("OvcEnrollmentForm").HasKey(c => c.Id);
        }
    }
}
