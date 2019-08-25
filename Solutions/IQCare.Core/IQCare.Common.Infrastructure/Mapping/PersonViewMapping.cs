using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonViewMapping : IEntityTypeConfiguration<PersonView>
    {
        public void Configure(EntityTypeBuilder<PersonView> builder)
        {
            builder.ToTable("PersonView").HasKey(c => c.Id);
        }
    }
}