using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonKinContactsViewMapping : IEntityTypeConfiguration<PersonKinContactsView>
    {
        public void Configure(EntityTypeBuilder<PersonKinContactsView> builder)
        {
            builder.ToTable("PersonKinContactsView").HasKey(c => c.Id);
        }
    }
}