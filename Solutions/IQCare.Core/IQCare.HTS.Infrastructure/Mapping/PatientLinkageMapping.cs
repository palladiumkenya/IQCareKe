using IQCare.HTS.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.HTS.Infrastructure.Mapping
{
    public class PatientLinkageMapping : IEntityTypeConfiguration<PatientLinkage>
    {
        public void Configure(EntityTypeBuilder<PatientLinkage> builder)
        {
            builder.ToTable("PatientLinkage")
                .HasKey(c => c.Id);
        }
    }
}