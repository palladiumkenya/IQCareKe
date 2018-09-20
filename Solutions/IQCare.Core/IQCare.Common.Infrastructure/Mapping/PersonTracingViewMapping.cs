using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonTracingViewMapping : IEntityTypeConfiguration<PersonTracingView>
    {
        public void Configure(EntityTypeBuilder<PersonTracingView> builder)
        {
            builder.ToTable("PersonTracingView").HasKey(c => c.Id);
        }
    }
}