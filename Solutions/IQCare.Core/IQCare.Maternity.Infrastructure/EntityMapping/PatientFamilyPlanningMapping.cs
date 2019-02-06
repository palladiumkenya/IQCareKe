using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientFamilyPlanningMapping : IEntityTypeConfiguration<PatientFamilyPlanning>
    {
        public void Configure(EntityTypeBuilder<PatientFamilyPlanning> builder)
        {
            builder.ToTable(nameof(PatientFamilyPlanning)).HasKey(x => x.Id);
        }
    }
}
