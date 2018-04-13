using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class MstPatientMapping : IEntityTypeConfiguration<MstPatient>
    {
        public void Configure(EntityTypeBuilder<MstPatient> builder)
        {
            builder.ToTable("mst_Patient").HasKey(c => c.Ptn_Pk);
        }
    }
}