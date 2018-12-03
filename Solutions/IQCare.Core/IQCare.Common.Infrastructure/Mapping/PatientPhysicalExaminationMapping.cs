using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class PatientPhysicalExaminationMapping : IEntityTypeConfiguration<PatientPhysicalExamination>
    {

        public void Configure(EntityTypeBuilder<PatientPhysicalExamination> builder)
        {
            builder.ToTable("PatientPhysicalExamination").HasKey(c => c.Id);
        }

    }

}
