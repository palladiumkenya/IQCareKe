using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class MaternalDrugAdministrationMapping : IEntityTypeConfiguration<MaternalDrugAdministration>
    {
        public void Configure(EntityTypeBuilder<MaternalDrugAdministration> builder)
        {
            builder.ToTable("PatientDrugAdministration")
                .Property(x => x.DateCreated).HasColumnName("CreateDate");
        }
    }

    public class PatientDrugAdministrationViewMapping : IEntityTypeConfiguration<PatientDrugAdministrationView>
    {
        public void Configure(EntityTypeBuilder<PatientDrugAdministrationView> builder)
        {
            builder.ToTable(nameof(PatientDrugAdministrationView))
                .Property(x => x.DateCreated).HasColumnName("CreateDate"); ;
        }
    }
}
