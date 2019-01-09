using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
    public class LookupMasterMapping : IEntityTypeConfiguration<LookupMaster>
    {

        public void Configure(EntityTypeBuilder<LookupMaster> builder)
        {
            builder.ToTable("LookupMaster").HasKey(c => c.Id);
        }
    }
}