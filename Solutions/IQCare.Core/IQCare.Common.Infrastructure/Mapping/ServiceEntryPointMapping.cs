using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class ServiceEntryPointMapping : IEntityTypeConfiguration<ServiceEntryPoint>
    {
        public void Configure(EntityTypeBuilder<ServiceEntryPoint> builder)
        {
            builder.ToTable("ServiceEntryPoint")
                .HasKey(e => e.Id);
        }
    }
}