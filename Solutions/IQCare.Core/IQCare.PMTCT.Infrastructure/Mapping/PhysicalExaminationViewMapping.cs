using IQCare.PMTCT.Core.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.PMTCT.Infrastructure.Mapping
{
    public class PhysicalExaminationViewMapping : IEntityTypeConfiguration<PhysicalExaminationView>
    {
        void IEntityTypeConfiguration<PhysicalExaminationView>.Configure(EntityTypeBuilder<PhysicalExaminationView> builder)
        {
            builder.ToTable("PhysicalExaminationView")
                .HasKey(c => c.Id);
        }
    }
}