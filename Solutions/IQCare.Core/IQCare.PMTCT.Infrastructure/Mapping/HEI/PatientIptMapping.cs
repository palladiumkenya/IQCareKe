using IQCare.PMTCT.Core.Models.HEI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping.HEI
{
    public class PatientIptMapping: IEntityTypeConfiguration<PatientIpt>
    {

        void IEntityTypeConfiguration<PatientIpt>.Configure(EntityTypeBuilder<PatientIpt> builder)
        {
            builder.ToTable("PatientIpt")
                .HasKey(c => c.Id);
        }
    }
}