using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientPncExercisesMapping : IEntityTypeConfiguration<PatientPncExercises>
    {
        public void Configure(EntityTypeBuilder<PatientPncExercises> builder)
        {
            builder.ToTable("PatientPncExercises").HasKey(x => x.Id);
        }
    }
}
