using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonIdentifierMapping : IEntityTypeConfiguration<PersonIdentifier>
    {
        void IEntityTypeConfiguration<PersonIdentifier>.Configure(EntityTypeBuilder<PersonIdentifier> builder)
        {
            builder.ToTable("PersonIdentifier")
                .HasKey(c => c.Id);
        }
    }
}