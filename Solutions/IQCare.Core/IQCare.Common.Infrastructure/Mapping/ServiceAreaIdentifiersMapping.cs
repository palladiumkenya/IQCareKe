using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class ServiceAreaIdentifiersMapping : IEntityTypeConfiguration<ServiceAreaIdentifiers>
    {
        public void Configure(EntityTypeBuilder<ServiceAreaIdentifiers> builder)
        {
            builder.ToTable("ServiceAreaIdentifiers").HasKey(c => c.Id);
        }
    }
}