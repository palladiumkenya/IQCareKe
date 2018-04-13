using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class DecodeMapping : IEntityTypeConfiguration<Decode>
    {
        public void Configure(EntityTypeBuilder<Decode> builder)
        {
            builder.ToTable("mst_Decode").HasKey(c => c.ID);
        }
    }
}