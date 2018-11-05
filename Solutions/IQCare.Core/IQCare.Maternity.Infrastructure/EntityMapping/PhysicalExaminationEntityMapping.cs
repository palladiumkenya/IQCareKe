using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    class PhysicalExaminationEntityMapping : IEntityTypeConfiguration<PhysicalExaminationView>
    {
        public void Configure(EntityTypeBuilder<PhysicalExaminationView> builder)
        {
            builder.ToTable(nameof(PhysicalExaminationView)).HasKey(x => x.Id);
        }
    }
}
