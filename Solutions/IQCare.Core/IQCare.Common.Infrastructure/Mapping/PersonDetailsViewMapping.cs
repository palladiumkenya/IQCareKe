using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonDetailsViewMapping : IEntityTypeConfiguration<PersonDetailsView>
    {
        public void Configure(EntityTypeBuilder<PersonDetailsView> builder)
        {
            builder.ToTable("PersonDetailsView").HasKey(c => c.Id);
        }
    }
}