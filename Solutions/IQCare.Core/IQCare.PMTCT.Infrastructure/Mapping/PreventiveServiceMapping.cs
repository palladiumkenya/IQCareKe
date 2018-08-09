using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PreventiveServiceMapping :IEntityTypeConfiguration<PreventiveService>
    {
        public void Configure(EntityTypeBuilder<PreventiveService> builder)
        {
            builder.ToTable("PatientPreventiveService")
                .HasKey(c => c.Id);
        }
    }
}