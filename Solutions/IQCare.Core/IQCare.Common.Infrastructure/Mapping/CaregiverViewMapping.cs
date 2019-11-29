using IQCare.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IQCare.Common.Infrastructure.Mapping
{
   public  class CaregiverViewMapping : IEntityTypeConfiguration<CaregiverView>
    {
        public void Configure(EntityTypeBuilder<CaregiverView> builder)
        {
            builder.ToTable("OVC_CaregiverView").HasKey(c => c.RowID);
        }
    }
}