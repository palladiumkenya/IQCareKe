using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PatientCounsellingMapping:IEntityTypeConfiguration<PatientEducation>
    {
        public void Configure(EntityTypeBuilder<PatientEducation> builder)
        {
            builder.ToTable("PatientCounselling")
                .HasKey(c => c.Id);
        }
    }
}