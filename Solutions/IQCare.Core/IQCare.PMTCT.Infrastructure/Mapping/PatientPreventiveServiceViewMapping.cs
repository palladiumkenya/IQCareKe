using IQCare.PMTCT.Core.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientPreventiveServiceViewMapping : IEntityTypeConfiguration<PatientPreventiveServiceView>
    {
        public void Configure(EntityTypeBuilder<PatientPreventiveServiceView> builder)
        {
            builder.ToTable("PatientPreventiveServiceView")
                .HasKey(c => c.Id);
        }
    }
}