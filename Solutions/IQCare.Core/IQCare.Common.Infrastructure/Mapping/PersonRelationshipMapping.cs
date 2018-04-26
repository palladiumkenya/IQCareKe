using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonRelationshipMapping : IEntityTypeConfiguration<PersonRelationship>
    {
        public void Configure(EntityTypeBuilder<PersonRelationship> builder)
        {
            builder.ToTable("PersonRelationship")
                .HasKey(c => c.Id);
        }
    }
}