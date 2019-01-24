using IQCare.Lab.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Lab.Infrastructure.Mapping
{
    public class LabOrderMapping : IEntityTypeConfiguration<LabOrder>
    {
        public void Configure(EntityTypeBuilder<LabOrder> builder)
        {
            builder.ToTable("ord_LabOrder").HasKey(c => c.Id);
            builder.ToTable("ord_LabOrder")
                .HasMany(x => x.LabOrderTests).WithOne(x => x.LabOrder);
        }
    }
}