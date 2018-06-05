using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientLookupViewMapping : IEntityTypeConfiguration<PatientLookupView>
    {
        public void Configure(EntityTypeBuilder<PatientLookupView> builder)
        {
            builder.ToTable("Api_PatientsView")
                .HasKey(c => c.RowID);
        }
    }
}