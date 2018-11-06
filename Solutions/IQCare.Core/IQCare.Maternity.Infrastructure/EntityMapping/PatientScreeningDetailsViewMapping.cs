using Microsoft.EntityFrameworkCore;
using IQCare.Maternity.Core.Domain.PNC;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class PatientScreeningDetailsViewMapping : IEntityTypeConfiguration<PatientScreeningDetailsView>
    {
        public void Configure(EntityTypeBuilder<PatientScreeningDetailsView> builder)
        {
            builder.ToTable(nameof(PatientScreeningDetailsView)).HasKey(x => x.Id);
        }
    }
}