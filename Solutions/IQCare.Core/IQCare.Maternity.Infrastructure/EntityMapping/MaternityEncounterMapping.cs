using IQCare.Maternity.Core.Domain.Maternity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Maternity.Infrastructure.EntityMapping
{
    public class MaternityEncounterMapping: IEntityTypeConfiguration<MaternityEncounter>
    {

        public void Configure(EntityTypeBuilder<MaternityEncounter> builder)
        {
            builder.ToTable("vw_Maternity-encounters").HasKey(x => x.Id);
        }
    }
}