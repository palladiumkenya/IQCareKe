using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class EmrMatrixMapping : IEntityTypeConfiguration<EmrMatrix>
    {
        public void Configure(EntityTypeBuilder<EmrMatrix> builder)
        {
            builder.ToTable("EmrMatrixDetails").HasKey(c => c.EmrName);
        }
    }
}