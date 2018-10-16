using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class PatientIptWorkupMapping : IEntityTypeConfiguration<PatientIptWorkup>
    {

        void IEntityTypeConfiguration<PatientIptWorkup>.Configure(EntityTypeBuilder<PatientIptWorkup> builder)
        {
            builder.ToTable("PatientIptWorkup")
                .HasKey(c => c.Id);
        }
    }
}