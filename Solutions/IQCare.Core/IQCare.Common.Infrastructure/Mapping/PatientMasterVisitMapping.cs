using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientMasterVisitMapping : IEntityTypeConfiguration<PatientMasterVisit>
    {
        public void Configure(EntityTypeBuilder<PatientMasterVisit> builder)
        {
            builder.ToTable("PatientMasterVisit")
                .HasKey(c => c.Id);
        }
    }
}