using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class PersonLocationViewMapping : IEntityTypeConfiguration<PersonLocationView>
    {
        public void Configure(EntityTypeBuilder<PersonLocationView> builder)
        {
            builder.ToTable("Api_PatientLocationView")
                .HasKey(c => c.PersonId);
        }
    }
}
