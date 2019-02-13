using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class OrdVisitMapping : IEntityTypeConfiguration<OrdVisit>
    {
        public void Configure(EntityTypeBuilder<OrdVisit> builder)
        {
            builder.ToTable("ord_Visit").HasKey(c => c.Visit_Id);
        }
    }
}