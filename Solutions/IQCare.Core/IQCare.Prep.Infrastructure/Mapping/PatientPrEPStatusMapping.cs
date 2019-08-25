using IQCare.Prep.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Prep.Infrastructure.Mapping
{
    public class PatientPrEPStatusMapping : IEntityTypeConfiguration<PatientPrEPStatus>
    {
        public void Configure(EntityTypeBuilder<PatientPrEPStatus> builder)
        {
            builder.ToTable("PatientPrEPStatus")
                .HasKey(c => c.Id);
        }
    }
}