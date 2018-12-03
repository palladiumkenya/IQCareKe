using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientRelationshipViewMapping : IEntityTypeConfiguration<PatientRelationshipView>
    {
        public void Configure(EntityTypeBuilder<PatientRelationshipView> builder)
        {
            builder.ToTable(nameof(PatientRelationshipView)).HasKey(x=>x.Id);
        }
    }
}
