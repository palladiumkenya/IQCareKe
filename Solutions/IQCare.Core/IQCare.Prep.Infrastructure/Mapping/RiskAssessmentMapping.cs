using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
   public  class RiskAssessmentMapping:IEntityTypeConfiguration<RiskAssessment>
    {
        public void Configure(EntityTypeBuilder<RiskAssessment> builder)
        {
            builder.ToTable("RiskAssessment")
                .HasKey(c => c.Id);
        }
    }
}
