using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PersonMaritalStatusMapping : IEntityTypeConfiguration<PersonMaritalStatus>
    {
        public void Configure(EntityTypeBuilder<PersonMaritalStatus> builder)
        {
            builder.ToTable("PatientMaritalStatus")
                .HasKey(c => c.Id);
        }
    }
}