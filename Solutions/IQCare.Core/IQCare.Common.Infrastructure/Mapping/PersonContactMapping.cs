using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonContactMapping : IEntityTypeConfiguration<PersonContact>
    {
        void IEntityTypeConfiguration<PersonContact>.Configure(EntityTypeBuilder<PersonContact> builder)
        {
            builder.ToTable("PersonContact")
                .HasKey(c => c.Id);
        }
    }
}