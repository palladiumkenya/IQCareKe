using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class OtzActivityFormMapping : IEntityTypeConfiguration<OtzActivityForm>
    {
        public void Configure(EntityTypeBuilder<OtzActivityForm> builder)
        {
            builder.ToTable("OtzActivityForm").HasKey(c => c.Id);
        }
    }
}
