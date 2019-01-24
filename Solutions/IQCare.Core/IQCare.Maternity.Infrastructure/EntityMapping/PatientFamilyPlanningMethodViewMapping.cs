using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientFamilyPlanningMethodViewMapping : IEntityTypeConfiguration<PatientFamilyPlanningMethodView>
    {
        public void Configure(EntityTypeBuilder<PatientFamilyPlanningMethodView> builder)
        {
            builder.ToTable(nameof(PatientFamilyPlanningMethodView)).HasKey(x => x.Id);
        }
    }
}
