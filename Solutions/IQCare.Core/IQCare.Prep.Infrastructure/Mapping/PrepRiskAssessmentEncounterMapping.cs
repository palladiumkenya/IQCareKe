using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PrepRiskAssessmentEncounterMapping : IEntityTypeConfiguration<PrepRiskAssessmentEncounterView>
    {
        public void Configure(EntityTypeBuilder<PrepRiskAssessmentEncounterView> builder)
        {
            builder.ToTable("PrepRiskAssessmentEncounterView")
                .HasKey(c => c.Id);
        }
    }
}
