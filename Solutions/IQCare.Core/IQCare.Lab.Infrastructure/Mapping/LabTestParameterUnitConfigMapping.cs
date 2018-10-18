using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabTestParameterUnitConfigMapping : IEntityTypeConfiguration<LabTestParameterUnit>
    {
        public void Configure(EntityTypeBuilder<LabTestParameterUnit> builder)
        {
            builder.ToTable("vw_LabTestParameterUnits").HasKey(c => c.UnitId);
        }
    }
}
