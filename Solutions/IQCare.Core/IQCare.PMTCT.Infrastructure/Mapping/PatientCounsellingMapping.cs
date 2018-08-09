using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientCounsellingMapping:IEntityTypeConfiguration<PatientCounselling>
    {
        public void Configure(EntityTypeBuilder<PatientCounselling> builder)
        {
            builder.ToTable("PatientCounselling")
                .HasKey(c => c.Id);
        }
    }
}