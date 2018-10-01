using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonIdentifierViewMapping : IEntityTypeConfiguration<PersonIdentifierView>
    {
        public void Configure(EntityTypeBuilder<PersonIdentifierView> builder)
        {
            builder.ToTable("PersonIdentifierView")
                .HasKey(c => c.Id);
        }
    }
}
