using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonPriorityMapping : IEntityTypeConfiguration<PersonPriority>
    {
        public void Configure(EntityTypeBuilder<PersonPriority> builder)
        {
            builder.ToTable("PersonPriority").HasKey(c => c.Id);
        }
    }
}